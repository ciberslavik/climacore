using Clima.Basics.Services.Communication.Messages;

namespace Clima.Basics.Services.Communication
{
    public interface INetworkInstaller
    {
        void InstallServices(INetworkServiceRegistrator registrator);
    }
}