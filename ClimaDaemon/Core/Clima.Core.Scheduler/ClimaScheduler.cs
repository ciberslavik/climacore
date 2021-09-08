﻿using System;
using System.Linq;
using System.Threading;
using Clima.Basics;
using Clima.Basics.Services;
using Clima.Core.Controllers;
using Clima.Core.Controllers.Heater;
using Clima.Core.Controllers.Ventilation;
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
        private readonly IHeaterController _heater;
        private readonly IVentilationController _ventilation;

        public ClimaScheduler(IControllerFactory controllerFactory, 
            ITimeProvider timeProvider,
            IHeaterController heater,
            IVentilationController ventilation)
        {
            _time = timeProvider;
            _heater = heater;
            _ventilation = ventilation;
            _controllerFactory = controllerFactory; 
            SchedulerState = new ShedulerStateObject();
        }

        public bool IsRunning => _isRunning;
        public ISystemLogger Log { get; set; }
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
           // throw new NotImplementedException();
        }

        public void SetSchedulerState(SchedulerState newState)
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
        }

        public ShedulerStateObject SchedulerState { get; }
        public void Start()
        {
            if (!_isRunning)
            {
                _isRunning = true;
            }
        }

        private void SchedulerProcess(object? o)
        {
            var sc = o as ClimaScheduler;
            
            Log.Debug("Process scheduler");
            TimeSpan workingTime = DateTime.Now - _startTime;
            int currentDay = workingTime.Days;
            Log.Debug("Process scheduler");
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
            if (_temperatureGraph.Points.Any(d => d.Day == dayNumber))
            {
                ValueByDayPoint? first = null;
                foreach (var d in _temperatureGraph.Points)
                {
                    if (d.Day == dayNumber)
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
                    where n1.Day < dayNumber
                    orderby n1.Day descending
                    select n1).First();

                var largerNumberCloseToInput = (from n1 in _temperatureGraph.Points
                    where n1.Day > dayNumber
                    orderby n1.Day
                    select n1).First();

                int periodDays = largerNumberCloseToInput.Day - smallerNumberCloseToInput.Day;
                float diff = dayNumber - smallerNumberCloseToInput.Day;
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
            
        }

        public void Init(object config)
        {
            if (config is SchedulerConfig cfg)
                _config = cfg;
            else
                return;
        }

        public Type ConfigType => typeof(SchedulerConfig);
        public ServiceState ServiceState { get; }
    }
}