using Clima.Basics.Configuration;

namespace Clima.Core.Scheduler.Configuration
{
    public class SchedulerConfig:IConfigurationItem
    {
        
        public int SchedulerPeriodSeconds { get; set; }
        public string ConfigurationName => nameof(SchedulerConfig);
    }
}