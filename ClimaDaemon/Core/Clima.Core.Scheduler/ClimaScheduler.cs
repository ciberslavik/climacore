using System;
using System.Linq;
using System.Threading;
using Clima.Basics;
using Clima.Basics.Services;
using Clima.Core.Controllers;
using Clima.Core.DataModel.GraphModel;
using Clima.Core.Scheduler.Configuration;

namespace Clima.Core.Scheduler
{
    public class ClimaScheduler : IClimaScheduler
    {
        private readonly IControllerFactory _controllerFactory;
        private Timer _schedulerTimer;
        private SchedulerConfig _config;
        private bool _isRunning;
        private DateTime _startTime;
        private GraphBase<ValueByDayPoint> _temperatureGraph;
        private GraphBase<MinMaxByDayPoint> _ventilationGraph;
        private readonly ITimeProvider _time;
        public ClimaScheduler(IControllerFactory controllerFactory, ITimeProvider timeProvider)
        {
            _time = timeProvider;
            _controllerFactory = controllerFactory; 
            SchedulerState = new ShedulerStateObject();
        }

        public bool IsRunning => _isRunning;
        
        public void SetTemperatureGraph(GraphBase<ValueByDayPoint> graph)
        {
            _temperatureGraph = graph;
        }

        public void SetVentilationGraph(GraphBase<MinMaxByDayPoint> graph)
        {
            _ventilationGraph = graph;
        }

        public void SetValveGraph(GraphBase<ValueByValuePoint> graph)
        {
            throw new NotImplementedException();
        }

        public ShedulerStateObject SchedulerState { get; }
        public void Start()
        {
            if (!_isRunning)
            {
                _schedulerTimer = new Timer(SchedulerProcess, null, 
                    _config.SchedulerPeriodSeconds,
                    _config.SchedulerPeriodSeconds);
            }
        }

        private void SchedulerProcess(object? o)
        {
            TimeSpan workingTime = DateTime.Now - _startTime;
            int currentDay = workingTime.Days;
            
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
         
            
            
            var result = new ValueByDayPoint();
            if (_temperatureGraph.Points.Any(d => d.DayNumber == dayNumber))
            {
                ValueByDayPoint? first = null;
                foreach (var d in _temperatureGraph.Points)
                {
                    if (d.DayNumber == dayNumber)
                    {
                        first = d;
                        break;
                    }
                }

                return first.Value;
            }
            else
            {
                var smallerNumberCloseToInput = (from n1 in _temperatureGraph.Points
                    where n1.DayNumber < dayNumber
                    orderby n1.DayNumber descending
                    select n1).First();

                var largerNumberCloseToInput = (from n1 in _temperatureGraph.Points
                    where n1.DayNumber > dayNumber
                    orderby n1.DayNumber
                    select n1).First();

                int periodDays = largerNumberCloseToInput.DayNumber - smallerNumberCloseToInput.DayNumber;
                float diff = dayNumber - smallerNumberCloseToInput.DayNumber;
                float point = diff / periodDays;
                float temperature = MathUtils.Lerp(
                    smallerNumberCloseToInput.Value,
                    largerNumberCloseToInput.Value,
                    point);

                return temperature;

            }
            
            return 0f;
        }
        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Init(object config)
        {
            if (config is SchedulerConfig cfg)
                _config = cfg;
            else
                return;
                
            
        }

        public Type ConfigType { get; }
        public ServiceState ServiceState { get; }
    }
}