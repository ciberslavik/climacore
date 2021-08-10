using System.Collections.Generic;
using Clima.Basics.Configuration;

namespace Clima.Core.Devices.Configuration
{
    public class DeviceProviderConfig : IConfigurationItem
    {
        private readonly Dictionary<string, MonitoredRelayConfig> _monitoredRelays =
            new Dictionary<string, MonitoredRelayConfig>();

        private readonly Dictionary<string, FrequencyConverterConfig> _frequencyConverters =
            new Dictionary<string, FrequencyConverterConfig>();

        private readonly Dictionary<string, FanConfig> _fans = new Dictionary<string, FanConfig>();
        private readonly Dictionary<string, ServoConfig> _servos = new Dictionary<string, ServoConfig>();

        public DeviceProviderConfig()
        {
        }

        public Dictionary<string, MonitoredRelayConfig> MonitoredRelays => _monitoredRelays;

        public Dictionary<string, FrequencyConverterConfig> FrequencyConverters => _frequencyConverters;
        public Dictionary<string, FanConfig> Fans => _fans;
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

            var fanConfig = new FanConfig();
            fanConfig.FanId = 0;
            fanConfig.FanName = "FAN:0";
            fanConfig.FanPriority = 1;
            fanConfig.FansCount = 2;
            fanConfig.Performance = 15000;
            fanConfig.StartPower = 0.1f;
            fanConfig.StopPower = 0.05f;

            fanConfig.FanType = FanType.Analog;
            fanConfig.FrequencyConverterName = "FC:0";
            config.Fans.Add(fanConfig.FanName, fanConfig);

            config.Servos.Add("SERVO:0", ServoConfig.CreateDefault(0));

            config.Sensors = SensorsConfig.CreateDefault();
            return config;
        }
    }
}