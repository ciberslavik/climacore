using System;
using System.Collections.Generic;
using Clima.Basics.Configuration;
using Clima.Core.Conrollers.Ventilation.Ventilation.Configuration;
using Clima.Core.Controllers.Ventilation;
using Clima.Core.Devices;

namespace Clima.Core.Conrollers.Ventilation.Ventilation
{
    public class FanFactory:IFanFactory
    {
        private readonly IDeviceProvider _deviceProvider;
        private FanFactoryConfig _config;
        private Dictionary<int, IFan> _fans;
        public FanFactory(IDeviceProvider deviceProvider)
        {
            _deviceProvider = deviceProvider;
            var configStore = ClimaContext.Current.ConfigurationStorage;
            
            if (configStore.Exist(FanFactoryConfig.FileName))
            {
                _config = ClimaContext.Current.ConfigurationStorage.GetConfig<FanFactoryConfig>(FanFactoryConfig.FileName);
            }
            else
            {
                _config = FanFactoryConfig.CreateDefaultConfig();
                configStore.RegisterConfig(_config.ConfigurationName, _config);
            }

            _fans = new Dictionary<int, IFan>();
        }
        
        public IAnalogFan GetAnalogFan(int fanId)
        {
            if (_fans.ContainsKey(fanId))
                return _fans[fanId] as IAnalogFan;

            var fanConfig = _config.GetConfig(fanId);
            if (fanConfig is null)
                throw new ArgumentException($"Configuration for fanId:{fanId} not found", nameof(fanId));
            if (fanConfig.FanType != FanType.Analog)
                throw new ArgumentException($"Configuration for fanId:{fanId} is not a AnalogFan", nameof(fanId));
            
            IAnalogFan fan = new AnalogFan(fanConfig);
            fan.FrequencyConverter = _deviceProvider.GetFrequencyConverter(fanConfig.FrequencyConverterName);

            return fan;
        }

        public IDiscreteFan GetDiscreteFan(int fanId)
        {
            throw new System.NotImplementedException();
        }
    }
}