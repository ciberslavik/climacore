using System;
using System.Linq;
using System.Threading;
using Clima.Basics;
using Clima.Basics.Services;
using Clima.Core.Controllers;
using Clima.Core.Controllers.Heater;
using Clima.Core.Controllers.Ventilation;
using Clima.Core.DataModel;
using Clima.Core.DataModel.GraphModel;
using Clima.Core.Devices;
using Clima.Core.Scheduler.Configuration;
using Clima.Core.Scheduler;
using Clima.Core.Scheduler.DataModel;

namespace Clima.Core.Scheduler
{
#nullable enable
    public partial class ClimaScheduler : IClimaScheduler
    {
        //private readonly IControllerFactory _controllerFactory;
        private readonly ITimeProvider _time;
        private readonly IHeaterController _heater;
        private readonly IVentilationController _ventilation;
        private readonly IGraphProviderFactory _graphProviderFactory;
        private readonly IDeviceProvider _deviceProvider;
        
        private SchedulerStateObject _state;
        
        private IServoDrive _valveServo;
        private IServoDrive _mineServo;
        
        private readonly object _threadLock = new object();
        private Timer _schedulerTimer;
        private bool _schedulerTimerRunning;


        private SchedulerConfig _config;
        private bool _isRunning;
       
        
        private GraphBase<ValueByDayPoint> _temperatureGraph;
        private GraphBase<MinMaxByDayPoint> _ventilationGraph;
        private GraphBase<ValueByValuePoint> _valveGraph;
        private GraphBase<ValueByValuePoint> _mineGraph;

        public ClimaScheduler(ITimeProvider timeProvider,
            IHeaterController heater,
            IVentilationController ventilation,
            IGraphProviderFactory graphProviderFactory,
            IDeviceProvider _deviceProvider)
        {
            _time = timeProvider;
            _heater = heater;
            _ventilation = ventilation;
            _graphProviderFactory = graphProviderFactory;
            this._deviceProvider = _deviceProvider;
            _schedulerTimerRunning = false;

            _state = new SchedulerStateObject();
            _state.SchedulerState = SchedulerState.Stopped;
            
            ServiceState = ServiceState.NotInitialized;
        }

        #region Properties

        public bool IsRunning => _isRunning;
        public ISystemLogger Log { get; set; }

        public Type ConfigType => typeof(SchedulerConfig);
        public ServiceState ServiceState { get; private set; }

        public SchedulerInfo SchedulerInfo
        {
            get
            {
                return new SchedulerInfo()
                {
                    TemperatureProfileKey = _temperatureGraph.Info.Key,
                    TemperatureProfileName = _temperatureGraph.Info.Name,
                    
                    VentilationProfileKey = _ventilationGraph.Info.Key,
                    VentilationProfileName = _ventilationGraph.Info.Name,
                    
                    ValveProfileKey = _valveGraph.Info.Key,
                    ValveProfileName =  _valveGraph.Info.Name,
                    
                    MineProfileKey = _mineGraph.Info.Key,
                    MineProfileName = _mineGraph.Info.Name,
                    
                    CurrentDay = this.CurrentDay,
                    CurrentHeads = this.CurrentHeads,
                    TemperatureSetPoint = _state.TemperatureSetPoint,
                    VentilationMaxPoint = _state.VentilationMaxPoint,
                    VentilationMinPoint = _state.VentilationMinPoint,
                    VentilationSetPoint = _state.VentilationSetPoint,
                    ValveSetPoint = _state.ValveSetPoint,
                    MineSetPoint = _state.MineSetPoint
                };
            }
        }

        public DateTime StartDate
        {
            get
            {
                if (_state.SchedulerState == SchedulerState.Preparing)
                    return _state.StartPreparingDate;
                else if (_state.SchedulerState == SchedulerState.Production)
                    return _state.StartProductionDate;
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        #endregion Properties

        #region Graph managment

        public void SetTemperatureProfile(string profileKey)
        {
            var tGraphProvider = _graphProviderFactory.TemperatureGraphProvider();
            if (tGraphProvider.ContainsKey(profileKey))
            {
                _temperatureGraph = tGraphProvider.GetGraph(profileKey);
                _config.TemperatureProfileKey = _temperatureGraph.Info.Key;
                Save();
            }
        }

        public void SetVentilationProfile(string profileKey)
        {
            var vGraphProvider = _graphProviderFactory.VentilationGraphProvider();
            if (vGraphProvider.ContainsKey(profileKey))
            {
                _ventilationGraph = vGraphProvider.GetGraph(profileKey);
                _config.VentilationProfileKey = _ventilationGraph.Info.Key;
                Save();
            }
        }
        public void SetValveProfile(string profileKey)
        {
            var vGraphProvider = _graphProviderFactory.ValveGraphProvider();
            if (vGraphProvider.ContainsKey(profileKey))
            {
                _valveGraph = vGraphProvider.GetGraph(profileKey);
                _config.ValveProfileKey = _valveGraph.Info.Key;
                Save();
            }
        }

        public void SetMineProfile(string profileKey)
        {
            var vGraphProvider = _graphProviderFactory.ValveGraphProvider();
            if (vGraphProvider.ContainsKey(profileKey))
            {
                _mineGraph = vGraphProvider.GetGraph(profileKey);
                _config.MineProfileKey = _mineGraph.Info.Key;
                Save();
            }
        }

        public void ReloadProfiles()
        {
            
        }
        #endregion Graph managment

        /*public void SetSchedulerState(SchedulerState newState)
        {
            if (SchedulerState.State == newState)
                return;
            switch (newState)
            {
                case Scheduler.SchedulerState.Stopped:
                    
                    break;
                case Scheduler.SchedulerState.Alarm:
                    
                    break;
                case Scheduler.SchedulerState.Cleaning:
                    
                    break;
                case Scheduler.SchedulerState.Brooding:
                    _schedulerTimer = new Timer(SchedulerProcess, this, 
                        _config.SchedulerPeriodSeconds * 1000,
                        _config.SchedulerPeriodSeconds * 1000);
                    break;
                case Scheduler.SchedulerState.Growing:
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }*/

        public void Start()
        {
            if (!_isRunning)
            {
                if((_state.SchedulerState == SchedulerState.Production)||(_state.SchedulerState == SchedulerState.Preparing))
                {
                    StartTimer();
                }
                _isRunning = true;
                ServiceState = ServiceState.Running;
            }
        }

        private void SchedulerProcess(object? o)
        {
            if (!(o is ClimaScheduler sc)) return;
            if (sc._state.StartProductionDate >= sc._time.Now)
            {
                var workingTime = _time.Now - sc._state.StartProductionDate;
                _state.CurrentDay = workingTime.Days;
            }
            else if (sc._state.StartPreProductionDate >= sc._time.Now &&
                     sc._state.StartProductionDate <= sc._time.Now)
            {
                _state.CurrentDay = 0;
            }

            Log.Debug("Process scheduler");
            string dataToLog = "";
            //Calculate setpoints
                //Temperature
            _state.TemperatureSetPoint = GetDayTemperature(_state.CurrentDay);
            dataToLog += "Temp set point:" + _state.TemperatureSetPoint + " \n";
                //Ventilation
            var dayVent = GetDayVentilation(_state.CurrentDay);
            _state.VentilationMaxPoint = dayVent.MaxValue;
            dataToLog += "Max vent:" + _state.VentilationMaxPoint + " \n";
            dataToLog += "Max m3:" + _state.VentilationMaxPoint * _state.CurrentHeads + "\n";
            
            _state.VentilationMinPoint = dayVent.MinValue;
            dataToLog += "Min vent:" + _state.VentilationMinPoint + " \n";
            dataToLog += "Min m3:" + _state.VentilationMinPoint * _state.CurrentHeads + "\n";
            
            _state.VentilationSetPoint = ProcessVent(
                _state.TemperatureSetPoint,
                _state.VentilationMinPoint,
                _state.VentilationMaxPoint);
            dataToLog += "Vent set point:" + _state.VentilationSetPoint + " \n";
            _state.VentilationInMeters = _state.VentilationSetPoint * _state.CurrentHeads;
            dataToLog += "Vent Real:" + _state.VentilationInMeters + "\n";
            
                //Valves
            _state.ValveSetPoint = GetCurrentValve(_state.VentilationSetPoint);
            dataToLog += "Valve set point:" + _state.ValveSetPoint + " \n";
            _state.MineSetPoint = GetCurrentMine(_state.VentilationSetPoint);
            dataToLog += "Mine set point:" + _state.MineSetPoint + " \n";
            
            Log.Info(dataToLog);
            //_state.ValveSetPoint = Get
            if (sc.SchedulerState == SchedulerState.Preparing)
            {
                sc._heater.Process(sc._pConfig.TemperatureSetPoint);
            }
            else if (sc.SchedulerState == SchedulerState.Production)
            {
                sc._heater.Process(_state.TemperatureSetPoint);
                
                sc._ventilation.SetPerformance((int)_state.VentilationInMeters);
                if(!sc._ventilation.ValveIsManual)
                    sc._ventilation.SetValvePosition(_state.ValveSetPoint);
                if(!sc._ventilation.MineIsManual)
                    sc._ventilation.SetMinePosition(_state.MineSetPoint);

            }
        }

        private float ProcessVent(float tempSetPoint, float minVent, float maxVent)
        {
            return minVent;
        }
        private float GetCurrentMinuteTemperature()
        {
            return 0f;
        }

        internal float GetDayTemperature(int dayNumber)
        {
            //Если текущего дня нет в графике, начинаем интерполяцию промежуточного значения между
            //соседними точками для текущего дня
            /*TimeSpan workingTime = DateTime.Now - SchedulerState.StartGrowingTime;
            int currentDay = workingTime.Days;*/
            if (dayNumber == 0)
            {
                return _temperatureGraph.Points[0].Value;
            }
            ValueByDayPoint? pt = _temperatureGraph.Points.FirstOrDefault(point => point.Day == dayNumber);
            if (pt != null)
                return pt.Value;


            var smallerNumberCloseToInput = (from n1 in _temperatureGraph.Points
                where n1.Day < dayNumber
                orderby n1.Day descending
                select n1).First();

            var largerNumberCloseToInput = (from n1 in _temperatureGraph.Points
                where n1.Day > dayNumber
                orderby n1.Day
                select n1).First();

            var periodDays = largerNumberCloseToInput.Day - smallerNumberCloseToInput.Day;
            float diff = dayNumber - smallerNumberCloseToInput.Day;
            var point = diff / periodDays;
            
            var temperature = MathUtils.Lerp(
                smallerNumberCloseToInput.Value,
                largerNumberCloseToInput.Value,
                point);

            return temperature;
        }
        internal float GetCurrentValve(float ventValue)
        {
            //Если текущего дня нет в графике, начинаем интерполяцию промежуточного значения между
            //соседними точками для текущего дня
            /*TimeSpan workingTime = DateTime.Now - SchedulerState.StartGrowingTime;
            int currentDay = workingTime.Days;*/
            ValueByValuePoint? pt = _valveGraph.Points.FirstOrDefault(point => point.ValueX == ventValue);
            if (pt != null)
                return pt.ValueY;


            var smallerNumberCloseToInput = (from n1 in _valveGraph.Points
                where n1.ValueX < ventValue
                orderby n1.ValueX descending
                select n1).First();

            var largerNumberCloseToInput = (from n1 in _valveGraph.Points
                where n1.ValueX > ventValue
                orderby n1.ValueX
                select n1).First();

            var distance = largerNumberCloseToInput.ValueX - smallerNumberCloseToInput.ValueX;
            float diff = ventValue - smallerNumberCloseToInput.ValueX;
            var point = diff / distance;
            
            var valve = MathUtils.Lerp(
                smallerNumberCloseToInput.ValueY,
                largerNumberCloseToInput.ValueY,
                point);

            return valve;
        }
        internal float GetCurrentMine(float ventValue)
        {
            //Если текущего дня нет в графике, начинаем интерполяцию промежуточного значения между
            //соседними точками для текущего дня
            /*TimeSpan workingTime = DateTime.Now - SchedulerState.StartGrowingTime;
            int currentDay = workingTime.Days;*/
            ValueByValuePoint? pt = _mineGraph.Points.FirstOrDefault(point => point.ValueX == ventValue);
            if (pt != null)
                return pt.ValueY;


            var smallerNumberCloseToInput = (from n1 in _mineGraph.Points
                where n1.ValueX < ventValue
                orderby n1.ValueX descending
                select n1).First();

            var largerNumberCloseToInput = (from n1 in _mineGraph.Points
                where n1.ValueX > ventValue
                orderby n1.ValueX
                select n1).First();

            var distance = largerNumberCloseToInput.ValueX - smallerNumberCloseToInput.ValueX;
            float diff = ventValue - smallerNumberCloseToInput.ValueX;
            var point = diff / distance;
            
            var valve = MathUtils.Lerp(
                smallerNumberCloseToInput.ValueY,
                largerNumberCloseToInput.ValueY,
                point);

            return valve;
        }
        internal MinMaxByDayPoint GetDayVentilation(int dayNumber)
        {
            //Если текущего дня нет в графике, начинаем интерполяцию промежуточного значения между
            //соседними точками для текущего дня
            /*TimeSpan workingTime = DateTime.Now - SchedulerState.StartGrowingTime;
            int currentDay = workingTime.Days;*/
            if (dayNumber == 0)
                return _ventilationGraph.Points[0];
            
            MinMaxByDayPoint? pt = _ventilationGraph.Points.FirstOrDefault(dayPoint => dayPoint.Day == dayNumber);
            if (pt != null)
                return pt;


            var smallerNumberCloseToInput = (from n1 in _ventilationGraph.Points
                where n1.Day < dayNumber
                orderby n1.Day descending
                select n1).First();

            var largerNumberCloseToInput = (from n1 in _ventilationGraph.Points
                where n1.Day > dayNumber
                orderby n1.Day
                select n1).First();

            var periodDays = largerNumberCloseToInput.Day - smallerNumberCloseToInput.Day;
            float diff = dayNumber - smallerNumberCloseToInput.Day;
            var point = diff / periodDays;
            
            var minValue = MathUtils.Lerp(
                smallerNumberCloseToInput.MinValue,
                largerNumberCloseToInput.MinValue,
                point);
            var maxValue = MathUtils.Lerp(
                smallerNumberCloseToInput.MaxValue,
                largerNumberCloseToInput.MaxValue,
                point);

            return new MinMaxByDayPoint(dayNumber, minValue, maxValue);
        }
        public void Stop()
        {
            if (_isRunning)
            {
                _isRunning = false;
                ServiceState = ServiceState.Stopped;
            }
        }

        public void Init(object config)
        {
            if (config is SchedulerConfig cfg)
            {
                _config = cfg;
                _temperatureGraph = _graphProviderFactory.TemperatureGraphProvider()
                    .GetGraph(_config.TemperatureProfileKey);

                _ventilationGraph = _graphProviderFactory.VentilationGraphProvider()
                    .GetGraph(_config.VentilationProfileKey);
                
                _valveGraph = _graphProviderFactory.ValveGraphProvider()
                    .GetGraph(_config.ValveProfileKey);
                
                _mineGraph = _graphProviderFactory.ValveGraphProvider()
                    .GetGraph(_config.MineProfileKey);

                _state.SchedulerState = _config.LastSchedulerState;
                _state.StartProductionDate = _config.ProductionConfig.PlandingDate;
                _state.StartPreProductionDate = _config.ProductionConfig.StartDate;
                _state.StartPreparingDate = _config.PreparingConfig.StartDate;
                GetCurrentHeads();

                ServiceState = ServiceState.Initialized;
            }
        }

        private void Save()
        {
            ClimaContext.Current.SaveConfiguration();
        }
        private void StartTimer()
        {
            if(_schedulerTimerRunning)
                return;
            
            _schedulerTimer = new Timer(SchedulerProcess, this, Timeout.Infinite, Timeout.Infinite);
            _schedulerTimer.Change(TimeSpan.FromTicks(0), TimeSpan.FromSeconds(_config.SchedulerPeriodSeconds));
            _schedulerTimerRunning = true;
        }

        private void StopTimer(TimeSpan timeout)
        {
            lock (_threadLock)
            {
                if (_schedulerTimer is not null)
                {
                    ManualResetEvent waitHandle = new ManualResetEvent(false);
                    if (_schedulerTimer.Dispose(waitHandle))
                    {
                        if (!waitHandle.WaitOne(timeout))
                            throw new TimeoutException("Timeout waiting for sceduler timer stop");
                    }

                    waitHandle.Close();
                    _schedulerTimer = null;
                    _schedulerTimerRunning = false;
                }
            }
        }
#nullable disable
    }
}