using System;
using System.Threading;
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

        public ClimaScheduler(IControllerFactory controllerFactory)
        {
            _controllerFactory = controllerFactory;
        }
        public void SetTemperatureGraph(GraphBase<ValueByDayPoint> graph)
        {
            throw new NotImplementedException();
        }

        public ShedulerStateObject SchedulerState { get; }
        public void Start()
        {
            _schedulerTimer = new Timer(SchedulerProcess, null, _config.SchedulerPeriodSeconds,
                _config.SchedulerPeriodSeconds);
        }

        private void SchedulerProcess(object? o)
        {
            
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