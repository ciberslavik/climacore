using System;
using System.Collections.Generic;
using Clima.Basics.Configuration;

namespace Clima.Core.Scheduler.Configuration
{
    public class SchedulerConfig:IConfigurationItem
    {
        
        public int SchedulerPeriodSeconds { get; set; }
        
        public DateTime StartCurrentStateTime { get; set; }
        public string ConfigurationName => nameof(SchedulerConfig);
        public List<LivestockOperation> LivestockOperations { get; set; }
        
        public ProductionState LastProductionState { get; set; }
        public PreparingConfig Preparing { get; set; }
    }
}