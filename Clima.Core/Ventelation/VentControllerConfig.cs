using System.Collections.Generic;
using Clima.Services.Configuration;

namespace Clima.Core.Ventelation
{
    public class VentControllerConfig:ConfigItemBase
    {
        public VentControllerConfig()
        {
        }

        public List<DFanConfig> DFanConfigs { get; set; } = new List<DFanConfig>();
        public List<AFanConfig> AFanConfigs { get; set; } = new List<AFanConfig>();
    }
}