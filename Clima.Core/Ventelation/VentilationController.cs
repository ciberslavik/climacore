using System.Collections.Generic;
using Clima.Services.Configuration;
using Clima.Services.Devices;

namespace Clima.Core.Ventelation
{
    public class VentilationController
    {
        private readonly IDeviceFactory _deviceFactory;
        private VentControllerConfig _config;
        private List<IDiscreteFan> _discreteFans;
        private IAnalogFan _analog1;
        private IAnalogFan _analog2;
        public VentilationController(IDeviceFactory deviceFactory)
        {
            _deviceFactory = deviceFactory;
            _discreteFans = new List<IDiscreteFan>();
        }

        public void Init(VentControllerConfig config)
        {
            _config = config;
            foreach (var discreteFanConfig in _config.DiscreteFanConfigs)
            {
                var discreteFan = CreateDiscreteFan(discreteFanConfig);
                
                _discreteFans.Add(discreteFan);
            }

            foreach (var analogFanConfig in _config.AnalogFanConfigs)
            {
                var analogFan = CreateAnalogFan(analogFanConfig);
                
            }
        }

        private DiscreteFan CreateDiscreteFan(DiscreteFanConfig config)
        {
            Relay fanRelay = _deviceFactory.GetRelay(config.RelayName);
            var fan = new DiscreteFan(fanRelay);
            return fan;
        }

        private AnalogFan CreateAnalogFan(AnalogFanConfig config)
        {
            FrequencyConverter converter = _deviceFactory.GetFrequencyConverter(config.FCName);

            var fan = new AnalogFan(converter);
            return fan;
        }
        public void SetPerformance(double performance)
        {
            
        }
        public VentControllerConfig ControllerConfig { get; set; }
    }
}