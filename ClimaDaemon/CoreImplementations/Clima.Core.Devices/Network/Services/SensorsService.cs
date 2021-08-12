using System.Globalization;
using Clima.Basics.Services;
using Clima.Basics.Services.Communication;
using Clima.Core.Devices.Network.Messages;

namespace Clima.Core.Devices.Network.Services
{
    public class SensorsService : ISensorsService, INetworkService
    {
        private readonly IDeviceProvider _deviceProvider;
        private ISensors _sensors;
        public ISystemLogger Logger { get; set; }

        public SensorsService(IDeviceProvider deviceProvider)
        {
            _deviceProvider = deviceProvider;
            _sensors = _deviceProvider.GetSensors();
        }

        [ServiceMethod]
        public SensorsServiceReadResponse ReadSensors(SensorsServiceReadRequest request)
        {
            Logger.Debug($"Read sensors");
            var response = new SensorsServiceReadResponse()
            {
                FrontTemperature = _sensors.FrontTemperature.ToString(CultureInfo.InvariantCulture),
                RearTemperature = _sensors.RearTemperature.ToString(CultureInfo.InvariantCulture),
                OutdoorTemperature = _sensors.OutdoorTemperature.ToString(CultureInfo.InvariantCulture),
                Humidity = _sensors.Humidity.ToString(CultureInfo.InvariantCulture),
                Pressure = _sensors.Pressure.ToString(CultureInfo.InvariantCulture)
            };
            return response;
        }
    }
}