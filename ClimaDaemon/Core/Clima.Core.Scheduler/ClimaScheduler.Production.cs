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
            if (_context.State != SchedulerState.Preparing)
            {
                _context.State = SchedulerState.Preparing;
                StartTimer();
                _context.StartPreparingDate = config.StartDate;
                Save();
            }
        }

        public void StartProduction(ProductionConfig config)
        {
            if (_context.State != SchedulerState.Production)
            {
                _context.State = SchedulerState.Production;
                StartTimer();
                _config.ProductionConfig = config;
                
                _context.StartProductionDate = config.PlandingDate;
                _context.StartPreProductionDate = config.StartDate;
                LivestockPlanting(config.PlaceHeads, config.PlandingDate);
                
                _config.LastSchedulerState = _context.State;
                Save();
            }
        }

        public void StopProduction()
        {
            if (_context.State != SchedulerState.Stopped)
            {
                _context.State = SchedulerState.Stopped;
                //StopTimer(TimeSpan.FromSeconds(20));
                _heater.StopHeater();
                _ventilation.ProcessController(0);
                Save();
            }
        }

        public SchedulerState SchedulerState => _context.State;
        
        public int CurrentHeads => GetCurrentHeads();

        public int CurrentDay
        {
            get
            {
                if (SchedulerState == SchedulerState.Stopped)
                    return 0;

                var diff = _time.Now - _context.StartProductionDate;
                return diff.Days;
            }
        }
        
    }
}