using Clima.Services.Configuration;

namespace Clima.Services.Devices.Configs
{
    public class DeviceFactoryConfig:ConfigItemBase
    {
        public DeviceFactoryConfig()
        {
            DiscreteFanConfig = new DiscreteFanConfig();
        }
        public DiscreteFanConfig DiscreteFanConfig { get; set; } 
    }
}