using Clima.Basics.Services;
using Clima.Basics.Services.Communication;
using Clima.Basics.Services.Communication.Exceptions;
using Clima.Basics.Services.Communication.Messages;
using Clima.Core.Network.Messages;
using Clima.Core.Network.Services;

namespace GraphProviderServices
{
    public class GraphNetworkInstaller : INetworkInstaller
    {
        public GraphNetworkInstaller()
        {
        }


        public void InstallServices(INetworkServiceRegistrator registrator)
        {
            registrator.RegisterNetworkService<GraphProviderService.GraphProviderService>();
        }
    }
}