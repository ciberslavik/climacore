using System;
using System.Threading;
using Clima.Core.Scheduler;

namespace Clima.Core.Scheduler
{
    public partial class ClimaScheduler
    {
        
        private PreparingConfig _pConfig;
        public void StartPreparing(PreparingConfig config)
        {
            _pConfig = config;
            Log.Debug($"Start preparing:{_pConfig.TemperatureSetPoint}");
            if (_state.SchedulerState != SchedulerState.Preparing)
            {
                _state.SchedulerState = SchedulerState.Preparing;
                StartTimer();
                _state.StartPreparingDate = config.StartDate;
                Save();
            }
        }

        public void StartProduction(ProductionConfig config)
        {
            if (_state.SchedulerState != SchedulerState.Production)
            {
                _state.SchedulerState = SchedulerState.Production;
                StartTimer();
                _state.StartProductionDate = config.StartDate;
                Save();
            }
        }

        public void StopProduction()
        {
            if (_state.SchedulerState != SchedulerState.Stopped)
            {
                _state.SchedulerState = SchedulerState.Stopped;
                StopTimer(TimeSpan.FromSeconds(20));
                _heater.StopHeater();
                _ventilation.SetPerformance(0);
                Save();
            }
        }

        public SchedulerState SchedulerState => _state.SchedulerState;
        
        public int CurrentHeads => GetCurrentHeads();

        public int CurrentDay
        {
            get
            {
                if (SchedulerState == SchedulerState.Stopped)
                    return 0;

                var diff = _time.Now - _state.StartProductionDate;
                return diff.Days;
            }
        }
        
    }
}