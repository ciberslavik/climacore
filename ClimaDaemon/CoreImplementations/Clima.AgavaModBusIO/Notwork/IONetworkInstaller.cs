using Clima.Basics.Services.Communication;

namespace Clima.AgavaModBusIO.Notwork
{
    public class IONetworkInstaller:INetworkInstaller
    {
        public void InstallServices(INetworkServiceRegistrator registrator)
        {
            registrator.RegisterNetworkService<IONetworkService>();
        }
    }
}