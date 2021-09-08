using System;
using Clima.Basics.Configuration;

namespace Clima.Core.Scheduler.Configuration
{
    public class SchedulerConfig:IConfigurationItem
    {
        
        public int SchedulerPeriodSeconds { get; set; }
        
        public SchedulerState CurrentState { get; set; }
        public DateTime StartCurrentStateTime { get; set; }
        public string ConfigurationName => nameof(SchedulerConfig);
    }
}