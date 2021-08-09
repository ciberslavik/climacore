using Clima.Basics.Services;
using Clima.Basics.Services.Communication;
using Clima.Basics.Services.Communication.Exceptions;
using Clima.Basics.Services.Communication.Messages;
using Clima.Core.Devices.Network.Messages;
using Clima.Core.Devices.Network.Services;

namespace Clima.Core.Devices.Network
{
    public class DevicesNetworkInstaller : INetworkInstaller
    {
        public void InstallServices(INetworkServiceRegistrator registrator)
        {
            registrator.RegisterNetworkService<SensorsService>();
        }
    }
}