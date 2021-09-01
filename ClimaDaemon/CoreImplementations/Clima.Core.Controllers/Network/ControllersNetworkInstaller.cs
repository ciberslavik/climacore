using Clima.Basics.Services.Communication;
using Clima.Core.Controllers.Network.Services;

namespace Clima.Core.Conrollers.Ventilation.Network
{
    public class ControllersNetworkInstaller:INetworkInstaller
    {
        public ControllersNetworkInstaller()
        {
        }


        public void InstallServices(INetworkServiceRegistrator registrator)
        {
            registrator.RegisterNetworkService<LightControllerService>();
            registrator.RegisterNetworkService<VentilationControllerService>();
        }
    }
}