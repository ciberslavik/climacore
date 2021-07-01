using System.Collections.Generic;
using Clima.Services.Configuration;

namespace Clima.Core.Ventelation
{
    public class VentControllerConfig:ConfigItemBase
    {
        public VentControllerConfig()
        {
        }

        public List<DiscreteFanConfig> DiscreteFanConfigs { get; set; } = new List<DiscreteFanConfig>();
        public List<AnalogFanConfig> AnalogFanConfigs { get; set; } = new List<AnalogFanConfig>();
    }
}