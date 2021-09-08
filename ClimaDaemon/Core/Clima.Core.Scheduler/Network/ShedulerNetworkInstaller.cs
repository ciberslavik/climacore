using Clima.Basics.Services;
using Clima.Basics.Services.Communication;
using Clima.Basics.Services.Communication.Messages;
using Clima.Core.Scheduler.Network.Services;

namespace Clima.Core.Scheduler.Network
{
    public class ShedulerNetworkInstaller : INetworkInstaller
    {
        public ShedulerNetworkInstaller()
        {
        }


        public void InstallServices(INetworkServiceRegistrator registrator)
        {
            registrator.RegisterNetworkService<LivestockService>();
            registrator.RegisterNetworkService<ProductionService>();
        }
    }
}