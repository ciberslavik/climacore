using System.Collections.Generic;
using Clima.Services.Devices.Configs;

namespace Clima.DataModel.Configurations.IOSystem
{
    public class DeviceFactoryConfig:ConfigItemBase
    {
        public DeviceFactoryConfig()
        {
            FcConfigItems = new List<FrequencyConverterConfig>();
            RelayConfigItems = new List<RelayConfig>();
        }
        public List<FrequencyConverterConfig> FcConfigItems { get; set; }
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
        public FrequencyConverterConfig GetFCConfig(string converterName)
        {
            foreach (FrequencyConverterConfig config in FcConfigItems)
            {
                if (config.ConverterName.Equals(converterName))
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