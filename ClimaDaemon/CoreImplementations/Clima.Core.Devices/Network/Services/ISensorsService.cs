using Clima.Core.Devices.Network.Messages;

namespace Clima.Core.Devices.Network.Services
{
    public interface ISensorsService
    {
        SensorsServiceReadResponse ReadSensors(SensorsServiceReadRequest request);
    }
}