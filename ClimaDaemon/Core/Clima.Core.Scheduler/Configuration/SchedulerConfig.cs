using System;
using System.Collections.Generic;
using Clima.Basics.Configuration;
using Clima.Core.DataModel;
using Clima.Core.Scheduler;

namespace Clima.Core.Scheduler.Configuration
{
    public class SchedulerConfig:IConfigurationItem
    {

        public int SchedulerPeriodSeconds { get; set; } = 20;
        
        public DateTime StartCurrentStateTime { get; set; }
        public SchedulerState LastSchedulerState { get; set; } = SchedulerState.Stopped;
        public ProductionConfig ProductionConfig { get; set; } = new ProductionConfig();
        public PreparingConfig PreparingConfig { get; set; } = new PreparingConfig();
        public VentilationParams VentilationParams { get; set; } = new VentilationParams(){Proportional = 1};
        public string ConfigurationName => nameof(SchedulerConfig);
        public List<LivestockOperation> LivestockOperations { get; set; } = new List<LivestockOperation>();
        
        public string TemperatureProfileKey { get; set; }
        public string VentilationProfileKey { get; set; }
        public string ValveProfileKey { get; set; }
        public string MineProfileKey { get; set; }
        
        public static SchedulerConfig CreateDefault()
        {
            return new SchedulerConfig();
            
        }
    }
}