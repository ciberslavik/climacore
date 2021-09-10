using System;
using System.Threading;
using Clima.Core.Scheduler;

namespace Clima.Core.Scheduler
{
    public partial class ClimaScheduler
    {
        private SchedulerState _currentState;
        private PreparingConfig _pConfig;
        public void StartPreparing(PreparingConfig config)
        {
            _pConfig = config;
            Log.Debug($"Start preparing:{_pConfig.TemperatureSetPoint}");
            if (_currentState != SchedulerState.Preparing)
            {
                _currentState = SchedulerState.Preparing;
                StartTimer();
                _startDate = DateTime.Now;
            }
        }

        public void StartProduction()
        {
            if (_currentState != SchedulerState.Production)
            {
                _currentState = SchedulerState.Production;
                StartTimer();
                _startDate = DateTime.Now;
            }
        }

        public void StopProduction()
        {
            if (_currentState != SchedulerState.Stopped)
            {
                _currentState = SchedulerState.Stopped;
                StopTimer(TimeSpan.FromSeconds(20));
                _heater.StopHeater();
            }
        }

        public SchedulerState SchedulerState => _currentState;
        public DateTime StartDate => _startDate;
        public int CurrentHeads => GetCurrentHeads();

        public int CurrentDay
        {
            get
            {
                if (SchedulerState == SchedulerState.Stopped)
                    return 0;
                
                var diff = _time.Now - _startDate;
                return diff.Days;
            }
        }
        
    }
}