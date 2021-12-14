using System;
using System.Linq;
using System.Text;
using System.Threading;
using Clima.Basics;
using Clima.Basics.Services;
using Clima.Core.Controllers;
using Clima.Core.Controllers.Heater;
using Clima.Core.Controllers.Ventilation;
using Clima.Core.DataModel;
using Clima.Core.DataModel.GraphModel;
using Clima.Core.DataModel.History;
using Clima.Core.Devices;
using Clima.Core.Hystory;
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
        private readonly IHistoryService _historyService;

        private SchedulerContext _context;

        private readonly object _threadLock = new object();
        private Timer? _schedulerTimer;
        private bool _schedulerTimerRunning;


        private SchedulerConfig _config;
        private bool _isRunning;
       
        
        private TemperatureGraph _temperatureGraph = new TemperatureGraph();
        private VentilationGraph _ventilationGraph = new VentilationGraph();
        private ValvePerVentilationGraph _valveGraph = new ValvePerVentilationGraph();
        private ValvePerVentilationGraph _mineGraph = new ValvePerVentilationGraph();

        public ClimaScheduler(ITimeProvider timeProvider,
            IHeaterController heater,
            IVentilationController ventilation,
            IGraphProviderFactory graphProviderFactory,
            IDeviceProvider deviceProvider,
            IHistoryService historyService,
            ISystemLogger? logger = null)
        {
            _time = timeProvider;
            _heater = heater;
            _ventilation = ventilation;
            _graphProviderFactory = graphProviderFactory;
            _deviceProvider = deviceProvider;
            _historyService = historyService;
            _schedulerTimerRunning = false;

            _context = new SchedulerContext();
            _context.State = SchedulerState.Stopped;
            _config = SchedulerConfig.CreateDefault();
            ServiceState = ServiceState.NotInitialized;
            Log = logger ?? new LogFileWriter("ClimaScheduler.log");
        }

        #region Properties

        public bool IsRunning => _isRunning;
        private ISystemLogger Log { get; set; }

        public Type ConfigType => typeof(SchedulerConfig);
        public ServiceState ServiceState { get; private set; }

        public SchedulerProcessInfo SchedulerProcessInfo
        {
            get
            {
                return new SchedulerProcessInfo()
                {
                    CurrentDay = this.CurrentDay,
                    CurrentHeads = this.CurrentHeads,
                    TemperatureSetPoint = _context.SetPoints.Temperature,
                    VentilationMaxPoint = _context.SetPoints.VentilationMax,
                    VentilationMinPoint = _context.SetPoints.VentilationMin,
                    VentilationSetPoint = _context.SetPoints.Ventilation,
                    ValveSetPoint = _context.SetPoints.Valves,
                    MineSetPoint = _context.SetPoints.Mines
                };
            }
        }

        public SchedulerProfilesInfo SchedulerProfilesInfo
        {
            get
            {
                return new SchedulerProfilesInfo()
                {
                    TemperatureProfileKey = _temperatureGraph.Info.Key,
                    TemperatureProfileName = _temperatureGraph.Info.Name,
                    
                    VentilationProfileKey = _ventilationGraph.Info.Key,
                    VentilationProfileName = _ventilationGraph.Info.Name,
                    
                    ValveProfileKey = _valveGraph.Info.Key,
                    ValveProfileName =  _valveGraph.Info.Name,
                    
                    MineProfileKey = _mineGraph.Info.Key,
                    MineProfileName = _mineGraph.Info.Name,
                };
            }
        }

        public DateTime StartDate
        {
            get
            {
                if (_context.State == SchedulerState.Preparing)
                    return _context.StartPreparingDate;
                else if (_context.State == SchedulerState.Production)
                    return _context.StartProductionDate;
                else
                {
                    return DateTime.MinValue;
                }
            }
        }
        
        public VentilationParams VentilationParameters
        {
            get => _config.VentilationParams;
            set
            {
                _config.VentilationParams = value;
                Save();
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
        
        public void Start()
        {
            if (!_isRunning)
            {
                if(_context.State is SchedulerState.Production or SchedulerState.Preparing)
                {
                    
                }
                else if(_context.State == SchedulerState.Stopped)
                {
                    _ventilation.Stop();
                }
                _isRunning = true;
                ServiceState = ServiceState.Running;
                StartTimer();
            }
        }

        private void LogContext(SchedulerContext context)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"Set points:\n");
            sb.Append($"\tTemperature:{context.SetPoints.Temperature}\n");
            sb.Append($"\tVent:{context.SetPoints.Ventilation} m3hph; {VentToReal(context.SetPoints.Ventilation)} m3ph\n");
            sb.Append($"\tMax Vent:{context.SetPoints.VentilationMax} m3hph; {VentToReal(context.SetPoints.VentilationMax)} m3ph\n");
            sb.Append($"\tMin Vent:{context.SetPoints.VentilationMin} m3hph; {VentToReal(context.SetPoints.VentilationMin)} m3ph\n");
            
            sb.Append($"\tValves:{Math.Round(context.SetPoints.Valves, 1)}%\n");
            sb.Append($"\tMines:{Math.Round(context.SetPoints.Mines, 1)}%\n");

            var sensors = ClimaContext.Current.Sensors;
            
            var historyPoint = new ClimatStateHystoryItem()
            {
                FrontTemperature = sensors.FrontTemperature,
                RearTemperature = sensors.RearTemperature,
                OutdoorTemperature = sensors.OutdoorTemperature,
                Pressure = sensors.Pressure,
                Humidity = sensors.Humidity,
                
                ValveSetPoint = context.SetPoints.Valves,
                ValvePosition = _ventilation.ValveCurrentPos,
                
                MineSetPoint = context.SetPoints.Mines,
                MinePosition = _ventilation.MineCurrentPos,
                
                VentilationSetPoint = context.SetPoints.Ventilation,
                TemperatureSetPoint = context.SetPoints.Temperature,
                PointDate = DateTime.Now
            };
            _historyService.AddClimatPoint(historyPoint);
        }

        private int VentToReal(float value)
        {
            return (int)Math.Round(value * _context.CurrentHeads, 0);
        }
        private void SchedulerProcess(object? o)
        {
            if (!(o is SchedulerContext context)) return;
            
            if (context.StartProductionDate <= _time.Now)
            {
                var workingTime = _time.Now - context.StartProductionDate;
                context.CurrentDay = workingTime.Days;
            }
            else if (context.StartPreProductionDate <= _time.Now &&
                     context.StartProductionDate >= _time.Now)
            {
                context.CurrentDay = 0;
            }

            Log.Debug("Process scheduler");
            if (context.State == SchedulerState.Production)
            {
                //Calculate setpoints
                //Temperature
                context.SetPoints.Temperature = _temperatureGraph.GetDayPoint(context.CurrentDay).Value;
                
                //Ventilation graph min max
                var dayVent = _ventilationGraph.GetDayPoint(context.CurrentDay);
                
                context.SetPoints.VentilationMax = dayVent.MaxValue;
                context.SetPoints.VentilationMin = dayVent.MinValue;
                //Ventilation setpoint
                context.SetPoints.Ventilation = ProcessVent(context.SetPoints.Temperature,
                    minVent:context.SetPoints.VentilationMin,
                    maxVent:context.SetPoints.VentilationMax);
                
                var ventilationInMeters = context.SetPoints.Ventilation * context.CurrentHeads;

                var ventPercent = VentToReal(context.SetPoints.Ventilation) / _ventilation.TotalPerformance * 100;

                //Valves

                context.SetPoints.Valves = GetCurrentValve(ventPercent);
                context.SetPoints.Mines = GetCurrentMine(ventPercent);

                LogContext(context);

                _heater.Process(_context.SetPoints.Temperature);

                _ventilation.ProcessController(VentToReal(_context.SetPoints.Ventilation));


                if (!_ventilation.ValveIsManual)
                    _ventilation.SetValvePosition(context.SetPoints.Valves);

                if (!_ventilation.MineIsManual)
                    _ventilation.SetMinePosition(context.SetPoints.Mines);
            }
            else if (context.State == SchedulerState.Stopped)
            {
                _ventilation.ProcessController(0);
                _ventilation.Stop();
            }
        }

        private float ProcessVent(float tempSetPoint, float minVent, float maxVent)
        {
            //Calculate temperature controller
            var currFront = _deviceProvider.GetSensors().FrontTemperature;
            var currRear = _deviceProvider.GetSensors().RearTemperature;

            var currAvg = (currFront + currRear) / 2;   //Average temperature

            var error = (currAvg - tempSetPoint);// * _config.VentilationParams.Proportional; //temperature error with proportional coefficient
            
            
            var ventDiff = maxVent - minVent;

            var inPercent = (ventDiff / 100) * error;
            
            var result = minVent + inPercent;
            
            //Ограничение выхода
            if (result < minVent)
                result = minVent;
            if (result > maxVent)
                result = maxVent;
            
            
            return result;
        }
        private float GetCurrentMinuteTemperature()
        {
            return 0f;
        }

        /*private float GetDayTemperature(int dayNumber)
        {
            //Если текущего дня нет в графике, начинаем интерполяцию промежуточного значения между
            //соседними точками для текущего дня
            /*TimeSpan workingTime = DateTime.Now - SchedulerState.StartGrowingTime;
            int currentDay = workingTime.Days;#1#
            /*if (dayNumber == 0)
            {
                return _temperatureGraph.Points[0].Value;
            }#1#
            ValueByDayPoint? pt = _temperatureGraph.Points.FirstOrDefault(point => point.Day == dayNumber);
            if (pt != null)
                return pt.Value;

            if (_temperatureGraph is TemperatureGraph tGraph)
            {
                tGraph.ContainsDay(15);
                tGraph.ContainsDay(0);
                tGraph.ContainsDay(1);
                tGraph.ContainsDay(20);
                tGraph.ContainsDay(21);
                tGraph.ContainsDay(22);
            }
            var smallerNumberCloseToInput = (from n1 in _temperatureGraph.Points
                where n1.Day < dayNumber
                orderby n1.Day descending
                select n1).First();

            var largerNumberCloseToInput = (from n1 in _temperatureGraph.Points
                where n1.Day > dayNumber
                orderby n1.Day
                select n1).FirstOrDefault();

            var periodDays = largerNumberCloseToInput.Day - smallerNumberCloseToInput.Day;
            float diff = dayNumber - smallerNumberCloseToInput.Day;
            var point = diff / periodDays;
            
            var temperature = MathUtils.Lerp(
                smallerNumberCloseToInput.Value,
                largerNumberCloseToInput.Value,
                point);

            return temperature;
        }*/
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

                _context.State = _config.LastSchedulerState;
                _context.StartProductionDate = _config.ProductionConfig.PlandingDate;
                _context.StartPreProductionDate = _config.ProductionConfig.StartDate;
                _context.StartPreparingDate = _config.PreparingConfig.StartDate;
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
            if (_schedulerTimer is null)
            {
                _schedulerTimer = new Timer(SchedulerProcess, _context, Timeout.Infinite, Timeout.Infinite);
                _schedulerTimer.Change(TimeSpan.FromTicks(0), TimeSpan.FromSeconds(_config.SchedulerPeriodSeconds));
                _schedulerTimerRunning = true;
            }
        }

        private void StopTimer(TimeSpan timeout)
        {
            if(_schedulerTimer is not null)
                lock (_threadLock)
                {
                    {
                        ManualResetEvent waitHandle = new ManualResetEvent(false);
                        if (_schedulerTimer.Dispose(waitHandle))
                        {
                            if (!waitHandle.WaitOne(timeout))
                                throw new TimeoutException("Timeout waiting for sceduler timer stop");
                        }

                        waitHandle.Close();
                        _schedulerTimerRunning = false;
                        _schedulerTimer = null;
                    }
                }
        }
        
        
#nullable disable
    }
}