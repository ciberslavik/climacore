using System.Collections.Generic;
using Clima.Basics.Configuration;

namespace Clima.Core.Devices.Configuration
{
    public class DeviceProviderConfig:IConfigurationItem
    {
        public DeviceProviderConfig()
        {
        }

        public Dictionary<string, MonitoredRelayConfig> MonitoredRelays { get; } = new Dictionary<string, MonitoredRelayConfig>();
        public string ConfigurationName => "DeviceProvider";
    }
}