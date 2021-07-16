using System.Collections.Generic;
using Clima.Basics.Configuration;

namespace Clima.Core.Devices.Configuration
{
    public class DeviceProviderConfig:IConfigurationItem
    {
        public DeviceProviderConfig()
        {
        }

        public List<MonitoredRelayConfig> MonitoredRelays { get; } = new List<MonitoredRelayConfig>();
        public string ConfigurationName => "DeviceProvider";
    }
}