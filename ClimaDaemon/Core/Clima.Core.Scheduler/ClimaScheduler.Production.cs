using System;
using System.Threading;
using Clima.Core.Scheduler;

namespace Clima.Core.Scheduler
{
    public partial class ClimaScheduler
    {
        private ProductionState _currentState;
        private PreparingConfig _pConfig;
        public void StartPreparing(PreparingConfig config)
        {
            _pConfig = config;
            Log.Debug($"Start preparing:{_pConfig.TemperatureSetPoint}");
            if (_currentState != ProductionState.Preparing)
            {
                _currentState = ProductionState.Preparing;
                _schedulerTimer = new Timer(SchedulerProcess, this, 
                    _config.SchedulerPeriodSeconds * 1000,
                    _config.SchedulerPeriodSeconds * 1000);
                
                _startTime = DateTime.Now;
            }
        }

        public void StartProduction()
        {
            if (_currentState != ProductionState.Production)
            {
                _currentState = ProductionState.Production;
            }
        }

        public void StopProduction()
        {
            if (_currentState != ProductionState.Stopped)
            {
                _currentState = ProductionState.Stopped;
                
            }
        }

        public ProductionState ProductionState => _currentState;
    }
}