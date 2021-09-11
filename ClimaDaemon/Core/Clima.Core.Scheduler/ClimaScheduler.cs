using System;
using System.Linq;
using System.Threading;
using Clima.Basics;
using Clima.Basics.Services;
using Clima.Core.Controllers;
using Clima.Core.Controllers.Heater;
using Clima.Core.Controllers.Ventilation;
using Clima.Core.DataModel.GraphModel;
using Clima.Core.Scheduler.Configuration;
using Clima.Core.Scheduler;

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

        private readonly object _threadLock = new object();
        private Timer _schedulerTimer;
        private bool _schedulerTimerRunning;


        private SchedulerConfig _config;
        private bool _isRunning;
        private DateTime _startDate;
        private GraphBase<ValueByDayPoint> _temperatureGraph;
        private GraphBase<MinMaxByDayPoint> _ventilationGraph;
        private GraphBase<ValueByValuePoint> _valveGraph;


        public ClimaScheduler(ITimeProvider timeProvider,
            IHeaterController heater,
            IVentilationController ventilation,
            IGraphProviderFactory graphProviderFactory)
        {
            _time = timeProvider;
            _heater = heater;
            _ventilation = ventilation;
            _graphProviderFactory = graphProviderFactory;
            _schedulerTimerRunning = false;
            _currentState = SchedulerState.Stopped;
            
            ServiceState = ServiceState.NotInitialized;

            _temperatureGraph = _graphProviderFactory.TemperatureGraphProvider().GetGraph("Default");
        }

        #region Properties

        public bool IsRunning => _isRunning;
        public ISystemLogger Log { get; set; }

        public Type ConfigType => typeof(SchedulerConfig);
        public ServiceState ServiceState { get; private set; }

        #endregion Properties

        #region Graph managment

        public void SetTemperatureGraph(string graph)
        {
            var tGraphProvider = _graphProviderFactory.TemperatureGraphProvider();
            if (tGraphProvider.ContainsKey(graph))
            {
                _temperatureGraph = tGraphProvider.GetGraph(graph);
                TemperatureGraph = graph;
            }
        }

        public string TemperatureGraph { get; private set; }

        public void SetVentilationGraph(string graph)
        {
            var vGraphProvider = _graphProviderFactory.VentilationGraphProvider();
            if (vGraphProvider.ContainsKey(graph))
            {
                _ventilationGraph = vGraphProvider.GetGraph(graph);
                VentilationGraph = graph;
            }
        }

        public string VentilationGraph { get; private set; }

        public void SetValveGraph(string graph)
        {
            var vGraphProvider = _graphProviderFactory.ValveGraphProvider();
            if (vGraphProvider.ContainsKey(graph))
            {
                _valveGraph = vGraphProvider.GetGraph(graph);
                ValveGraph = graph;
            }
        }

        public string ValveGraph { get; private set; }

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
                _isRunning = true;
                ServiceState = ServiceState.Running;
            }
        }

        private void SchedulerProcess(object? o)
        {
            if (!(o is ClimaScheduler sc)) return;
            var workingTime = DateTime.Now - sc._startDate;
            var currentDay = workingTime.Days;
            Log.Debug("Process scheduler");

            if (sc.SchedulerState == SchedulerState.Preparing)
            {
                sc._heater.Process(sc._pConfig.TemperatureSetPoint);
            }
            else if (sc.SchedulerState == SchedulerState.Production)
            {
                sc._heater.Process(GetDayTemperature(currentDay));
            }
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
        internal MinMaxByDayPoint GetDayVentilation(int dayNumber)
        {
            //Если текущего дня нет в графике, начинаем интерполяцию промежуточного значения между
            //соседними точками для текущего дня
            /*TimeSpan workingTime = DateTime.Now - SchedulerState.StartGrowingTime;
            int currentDay = workingTime.Days;*/
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
                ServiceState = ServiceState.Initialized;
            }
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