using Clima.Services.Devices;

namespace Clima.Core.Ventelation
{
    public class VentelationController
    {
        private readonly IDeviceFactory _deviceFactory;

        public VentelationController(IDeviceFactory deviceFactory)
        {
            _deviceFactory = deviceFactory;
        }

        public void SetPerformance(double performance)
        {
            
        }
        public VentControllerConfig ControllerConfig { get; set; }
    }
}