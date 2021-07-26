using System.Collections.Generic;
using Clima.Basics.Configuration;

namespace Clima.Core.Conrollers.Ventilation.Ventilation.Configuration
{
    public class FanFactoryConfig:IConfigurationItem
    {
        private Dictionary<int, FanConfig> _fans;

        public FanFactoryConfig()
        {
            _fans = new Dictionary<int, FanConfig>();
        }
        public Dictionary<int, FanConfig> Fans
        {
            get => _fans;
            set => _fans = value;
        }

        public string ConfigurationName => FileName;
        public static string FileName => "FanFactoryConfig";

        public FanConfig GetConfig(int fanId)
        {
            FanConfig config = null;
            foreach (var fan in _fans.Values)
            {
                if (fan.FanId == fanId)
                {
                    config = fan;
                    break;
                }
            }


            return config;
        }
        public static FanFactoryConfig CreateDefaultConfig()
        {
            var factoryConf = new FanFactoryConfig();

            for (int i = 0; i < 10; i++)
            {
                if (i < 2)
                    factoryConf.Fans.Add(i, CreateDefaultFanConfig(i, $"FAN:{i}", FanType.Analog));
                else
                    factoryConf.Fans.Add(i, CreateDefaultFanConfig(i, $"FAN:{i}", FanType.Discrete));
            }

            return factoryConf;
        }
        private static FanConfig CreateDefaultFanConfig(int fanId, string fanName, FanType fanType)
        {
            var config = new FanConfig();
            config.FanId = fanId;
            config.FanName = fanName;
            config.FanType = fanType;
            config.FanPriority = fanId;
            config.Hermetise = false;
            config.Disabled = false;
            
            if (fanType == FanType.Analog)
            {
                config.RelayName = "";
                config.FrequencyConverterName = $"FC:{fanId}";
                config.FanType = FanType.Analog;
                config.Performance = 15000;
                config.FansCount = 2;
                config.StartPower = 0.1;
                config.StopPower = 0.08;
            }
            else if (fanType == FanType.Discrete)
            {
                config.RelayName = $"REL:{fanId}";
                config.FrequencyConverterName = "";
                config.FanType = FanType.Discrete;
                config.Performance = 30000;
                config.FansCount = 1;
                config.StartPower = 0.20;
                config.StopPower = 0.15;
            }
            return config;
        }
    }
}