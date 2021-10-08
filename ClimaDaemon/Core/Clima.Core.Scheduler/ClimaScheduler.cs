﻿using System;
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

        private VentilationParams _ventilationParameters;
        private SchedulerStateObject _state;

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

        public SchedulerProcessInfo SchedulerProcessInfo
        {
            get
            {
                return new SchedulerProcessInfo()
                {
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
                if(_state.SchedulerState is SchedulerState.Production or SchedulerState.Preparing)
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
            
            if (sc._state.StartProductionDate <= sc._time.Now)
            {
                var workingTime = _time.Now - sc._state.StartProductionDate;
                sc._state.CurrentDay = workingTime.Days;
            }
            else if (sc._state.StartPreProductionDate <= sc._time.Now &&
                     sc._state.StartProductionDate >= sc._time.Now)
            {
                sc._state.CurrentDay = 0;
            }

            Log.Debug("Process scheduler");
            string dataToLog = "";
            //Calculate setpoints
                //Temperature
            sc._state.TemperatureSetPoint = GetDayTemperature(sc._state.CurrentDay);
            dataToLog += "Temp set point:" + sc._state.TemperatureSetPoint + " \n";
                //Ventilation
            var dayVent = GetDayVentilation(sc._state.CurrentDay);
            sc._state.VentilationMaxPoint = dayVent.MaxValue;
            dataToLog += "\tMax vent:" + sc._state.VentilationMaxPoint + " \n";
            dataToLog += "\tMax m3:" + sc._state.VentilationMaxPoint * sc._state.CurrentHeads + "\n";
            
            sc._state.VentilationMinPoint = dayVent.MinValue;
            dataToLog += "\tMin vent:" + sc._state.VentilationMinPoint + " \n";
            dataToLog += "\tMin m3:" + sc._state.VentilationMinPoint * sc._state.CurrentHeads + "\n";

            sc._state.VentilationInMeters = ProcessVent(
                sc._state.TemperatureSetPoint,
                sc._state.VentilationMinPoint * sc._state.CurrentHeads,
                sc._state.VentilationMaxPoint * sc._state.CurrentHeads);
            sc._state.VentilationSetPoint = sc._state.VentilationInMeters / sc._state.CurrentHeads;
            
            dataToLog += "\tVent set point:" + sc._state.VentilationSetPoint + " \n";
            
            dataToLog += "\tVent Real:" + _state.VentilationInMeters + "\n";
            var ventPercent = sc._state.VentilationInMeters / _ventilation.TotalPerformance * 100;
            dataToLog += "\tVent percent:" + ventPercent + "\n";
                //Valves
            
            _state.ValveSetPoint = GetCurrentValve(ventPercent);
            dataToLog += "\tValve set point:" + _state.ValveSetPoint + " \n";
            _state.MineSetPoint = GetCurrentMine(ventPercent);
            dataToLog += "\tMine set point:" + _state.MineSetPoint + " \n";
            
            Log.Info(dataToLog);
            //_state.ValveSetPoint = Get
            if (sc.SchedulerState == SchedulerState.Preparing)
            {
                sc._heater.Process(sc._pConfig.TemperatureSetPoint);
            }
            else if (sc.SchedulerState == SchedulerState.Production)
            {
                sc._heater.Process(_state.TemperatureSetPoint);
                
                sc._ventilation.ProcessController((int)_state.VentilationInMeters);
                
                if(!sc._ventilation.ValveIsManual)
                    sc._ventilation.SetValvePosition(_state.ValveSetPoint);
                
                if(!sc._ventilation.MineIsManual)
                    sc._ventilation.SetMinePosition(_state.MineSetPoint);
            }
        }

        private float ProcessVent(float tempSetPoint, float minVent, float maxVent)
        {
            //Calculate temperature controller
            var currFront = _deviceProvider.GetSensors().FrontTemperature;
            var currRear = _deviceProvider.GetSensors().RearTemperature;

            var currAvg = (currFront + currRear) / 2;
            var error = (currAvg - tempSetPoint) * _config.VentilationParams.Proportional;
            
            

            var ventDiff = maxVent - minVent;

            var inPercent = (ventDiff / 100) * error;
            

            var result = minVent + inPercent;
            if (result < minVent)
                result = minVent;
            if (result > maxVent)
                result = maxVent;
            Log.Debug($"t avg:{currAvg} setpoint:{tempSetPoint} min:{minVent} max:{maxVent}\n\terror:{error} "+
                      $"\tP:{_config.VentilationParams.Proportional}\n"+
                      $"\tpercent: {inPercent} diff:{ventDiff} \n\tresult:{result}");
            return result;
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
                select n1).FirstOrDefault();

            var largerNumberCloseToInput = (from n1 in _ventilationGraph.Points
                where n1.Day > dayNumber
                orderby n1.Day
                select n1).FirstOrDefault();

            if (largerNumberCloseToInput is null)
            {
                if (smallerNumberCloseToInput is null)
                {
                    return new MinMaxByDayPoint(dayNumber, _ventilationGraph.Points.First().MinValue,
                        _ventilationGraph.Points.First().MaxValue);
                }
                else
                {
                    return new MinMaxByDayPoint(dayNumber, smallerNumberCloseToInput.MinValue,
                        smallerNumberCloseToInput.MaxValue);
                }
            }

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