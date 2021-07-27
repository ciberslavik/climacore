using System.Collections.Generic;
using Clima.Basics.Configuration;

namespace Clima.Core.Devices.Configuration
{
    public class DeviceProviderConfig:IConfigurationItem
    {
        private readonly Dictionary<string, MonitoredRelayConfig> _monitoredRelays = new Dictionary<string, MonitoredRelayConfig>();
        private readonly Dictionary<string, FrequencyConverterConfig> _frequencyConverters = new Dictionary<string, FrequencyConverterConfig>();

        public DeviceProviderConfig()
        {
        }
        
        public Dictionary<string, MonitoredRelayConfig> MonitoredRelays => _monitoredRelays;

        public Dictionary<string, FrequencyConverterConfig> FrequencyConverters => _frequencyConverters;

        public string ConfigurationName => FileName;

        internal static string FileName => "DeviceProviderConfig";

        internal static DeviceProviderConfig CreateDefault()
        {
            var config = new DeviceProviderConfig();
            for (int i = 0; i < 2; i++)
            {
                var converter = new FrequencyConverterConfig();
                var convName = $"FC:{i}";
                converter.ConverterName = convName;
                converter.AnalogPinName = $"AO:1:{i}";
                converter.ConverterType = ConverterType.Thyristor;
                
                config._frequencyConverters.Add(convName, converter);
            }
            for (int i = 0; i < 8; i++)
            {
                var relay = new MonitoredRelayConfig();
                var relayName = $"REL:{i}";
                relay.RelayName = relayName;
                relay.ControlPinName = $"DO:2:{i}";
                relay.MonitorPinName = $"DI:2:{i}";
                config._monitoredRelays.Add(relayName, relay);
            }
            return config;
        }
    }
}