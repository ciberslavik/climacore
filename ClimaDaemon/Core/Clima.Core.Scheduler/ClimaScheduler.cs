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
        public ClimaScheduler(IControllerFactory controllerFactory)
        {
            _controllerFactory = controllerFactory; 
            SchedulerState = new ShedulerStateObject();
        }

        public bool IsRunning => _isRunning;
        
        public void SetTemperatureGraph(GraphBase<ValueByDayPoint> graph)
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
        private ValueByDayPoint GetCurrentDayTemperature()
        {
            //Если текущего дня нет в графике, начинаем интерполяцию промежуточного значения между
            //соседними точками для текущего дня
            TimeSpan workingTime = DateTime.Now - SchedulerState.StartGrowingTime;
            int currentDay = workingTime.Days;
         
            
            var smallerNumberCloseToInput = (from n1 in _temperatureGraph.Points
                where n1.DayNumber <= currentDay
                orderby n1.DayNumber descending
                select n1).First();

            var largerNumberCloseToInput = (from n1 in _temperatureGraph.Points
                where n1.DayNumber > currentDay
                orderby n1.DayNumber
                select n1).First();

            int periodDays = largerNumberCloseToInput.DayNumber - smallerNumberCloseToInput.DayNumber;
            float diff = currentDay - smallerNumberCloseToInput.DayNumber;
            float temperature = MathUtils.Lerp(
                largerNumberCloseToInput.Value,
                smallerNumberCloseToInput.Value,
                currentDay/periodDays);
            
            var result = new ValueByDayPoint();
            if (_temperatureGraph.Points.Any(d => d.DayNumber == currentDay))
                return _temperatureGraph.Points.FirstOrDefault(d => d.DayNumber == currentDay);
            else
            {
                
                
            }
            
            return result;
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