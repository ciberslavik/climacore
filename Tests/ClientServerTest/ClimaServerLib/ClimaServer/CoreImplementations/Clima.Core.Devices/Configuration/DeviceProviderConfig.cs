using System.Collections.Generic;

namespace Clima.Core.Devices.Configuration
{
    public class DeviceProviderConfig
    {
        public DeviceProviderConfig()
        {
        }

        public List<MonitoredRelayConfig> MonitoredRelays { get; } = new List<MonitoredRelayConfig>();
    }
}