using System.Collections.Generic;
using Clima.Services.Configuration;

namespace Clima.Services.Devices.Configs
{
    public class DeviceFactoryConfig:ConfigItemBase
    {
        public DeviceFactoryConfig()
        {
            DiscreteFanConfig = new DiscreteFanConfig();
            FcConfig = new List<FCConfig>();
        }
        public DiscreteFanConfig DiscreteFanConfig { get; set; } 
        public List<FCConfig> FcConfig { get; set; }
    }
}