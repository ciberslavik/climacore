using System.Collections.Generic;
using Clima.Services.Configuration;
using Clima.Services.Devices.Configs;

namespace Clima.Services.Devices.FactoryConfigs
{
    public class DeviceFactoryConfig:ConfigItemBase
    {
        public DeviceFactoryConfig()
        {
            FcConfigItems = new List<FCConfig>();
            RelayConfigItems = new List<RelayConfig>();
        }
        public List<FCConfig> FcConfigItems { get; set; }
        public List<RelayConfig> RelayConfigItems { get; set; }

        public RelayConfig GetRelayConfig(string relayName)
        {
            foreach (RelayConfig config in RelayConfigItems)
            {
                if (config.RelayName.Equals(relayName))
                    return config;
            }

            return null;
        }

        public static DeviceFactoryConfig CreateDefault()
        {
            var cfg = new DeviceFactoryConfig();
            
            
            return cfg;
        }
    }
}