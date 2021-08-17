using Clima.Basics.Services.Communication;
using Clima.Core.Network.Services;

namespace Clima.Core.Network
{
    public class CoreNetworkInstaller:INetworkInstaller
    {
        public void InstallServices(INetworkServiceRegistrator registrator)
        {
            registrator.RegisterNetworkService<SystemStateService>();
        }
    }
}