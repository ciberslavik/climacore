using System;
using System.Collections.Generic;
using Clima.Basics.Configuration;
using Clima.Core.Scheduler;

namespace Clima.Core.Scheduler.Configuration
{
    public class SchedulerConfig:IConfigurationItem
    {

        public int SchedulerPeriodSeconds { get; set; } = 20;
        
        public DateTime StartCurrentStateTime { get; set; }
        public string ConfigurationName => nameof(SchedulerConfig);
        public List<LivestockOperation> LivestockOperations { get; set; } = new List<LivestockOperation>();

        public SchedulerState LastSchedulerState { get; set; } = SchedulerState.Stopped;
        public PreparingConfig Preparing { get; set; } = new PreparingConfig();

        public static SchedulerConfig CreateDefault()
        {
            return new SchedulerConfig();
            
        }
    }
}