using Clima.Basics.Services.Communication;
using Clima.Core.Devices;
using Clima.Core.Network.Messages;
using Clima.Core.Scheduler;

namespace Clima.Core.Network.Services
{
    public class SystemStateService:INetworkService
    {
        public SystemStateService(IClimaScheduler scheduler)
        {
            
        }

        [ServiceMethod]
        public ClimatStateResponse GetClimatState(DefaultRequest request)
        {
            var s = ClimaContext.Current.Sensors;
            var response = new ClimatStateResponse()
            {
                FrontTemperature = s.FrontTemperature,
                RearTemperature = s.RearTemperature,
                OutdoorTemperature = s.OutdoorTemperature,
                Humidity = s.Humidity,
                Pressure = s.Pressure,
                ValvePosition = s.Valve1,
                MinePosition = s.Valve2
            };
            

            return response;
        }
    }
}