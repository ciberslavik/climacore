using System.Collections.Generic;
using Clima.Basics.Configuration;
using Clima.Core.DataModel;

namespace Clima.Core.Devices.Configuration
{
    public class DeviceProviderConfig : IConfigurationItem
    {
        private readonly Dictionary<string, MonitoredRelayConfig> _monitoredRelays =
            new Dictionary<string, MonitoredRelayConfig>();

        private readonly Dictionary<string, FrequencyConverterConfig> _frequencyConverters =
            new Dictionary<string, FrequencyConverterConfig>();

        private readonly Dictionary<string, FanInfo> _fans = new Dictionary<string, FanInfo>();
        private readonly Dictionary<string, ServoConfig> _servos = new Dictionary<string, ServoConfig>();

        public DeviceProviderConfig()
        {
        }
        public Dictionary<string, HeaterConfig> Heaters { get; set; }= new Dictionary<string, HeaterConfig>();
        public Dictionary<string, MonitoredRelayConfig> MonitoredRelays => _monitoredRelays;

        public Dictionary<string, FrequencyConverterConfig> FrequencyConverters => _frequencyConverters;
        public Dictionary<string, FanInfo> Fans => _fans;
        public Dictionary<string, ServoConfig> Servos => _servos;
        public SensorsConfig Sensors { get; set; }
        public string ConfigurationName => FileName;

        internal static string FileName => "DeviceProviderConfig";

        internal static DeviceProviderConfig CreateDefault()
        {
            var config = new DeviceProviderConfig();
            for (var i = 0; i < 2; i++)
            {
                var converter = new FrequencyConverterConfig();
                var convName = $"FC:{i}";
                converter.ConverterName = convName;
                converter.AnalogPinName = $"AO:1:{i}";
                converter.ConverterType = ConverterType.Thyristor;

                config._frequencyConverters.Add(convName, converter);
            }

            for (var i = 0; i < 8; i++)
            {
                var relay = new MonitoredRelayConfig();
                var relayName = $"REL:{i}";
                relay.RelayName = relayName;
                relay.ControlPinName = $"DO:2:{i}";
                relay.MonitorPinName = $"DI:2:{i}";
                config._monitoredRelays.Add(relayName, relay);
            }
            
            
            config.Servos.Add("SERVO:0", ServoConfig.CreateDefault(0));
            config.Servos.Add("SERVO:1", ServoConfig.CreateDefault(1));

            config.Sensors = SensorsConfig.CreateDefault();
            config.Heaters.Add("HEAT:0", new HeaterConfig()
            {
                HeaterName = "HEAT:0",
                PinName = "DO:3:10"
            });
            config.Heaters.Add("HEAT:1", new HeaterConfig()
            {
                HeaterName = "HEAT:1",
                PinName = "DO:3:11"
            });
            return config;
        }
    }
}