namespace Clima.Core.Devices.Network.Services
{
    public class SensorsService
    {
        private readonly IDeviceProvider _deviceProvider;

        public SensorsService(IDeviceProvider deviceProvider)
        {
            _deviceProvider = deviceProvider;
        }
    }
}