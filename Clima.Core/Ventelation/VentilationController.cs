using System.Collections.Generic;
using Clima.Services.Configuration;
using Clima.Services.Devices;

namespace Clima.Core.Ventelation
{
    public class VentilationController
    {
        private readonly IDeviceFactory _deviceFactory;
        private VentControllerConfig _config;
        private List<DiscreteFan> _discreteFans;
        private AnalogFan _analog1;
        private AnalogFan _analog2;
        public VentilationController(IDeviceFactory deviceFactory)
        {
            _deviceFactory = deviceFactory;
            _discreteFans = new List<DiscreteFan>();
        }

        public void Init(VentControllerConfig config)
        {
            _config = config;
            foreach (var discreteFanConfig in _config.DiscreteFanConfigs)
            {
                var discreteFan = CreateDiscreteFan(discreteFanConfig);
                
                _discreteFans.Add(discreteFan);
            }
            
        }

        private DiscreteFan CreateDiscreteFan(DiscreteFanConfig config)
        {
            Relay fanRelay = _deviceFactory.GetRelay(config.RelayName);
            var fan = new DiscreteFan(fanRelay);
            return fan;
        }

        public void SetPerformance(double performance)
        {
            
        }
        public VentControllerConfig ControllerConfig { get; set; }
    }
}