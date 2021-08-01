using Clima.Basics.Services.Communication.Messages;

namespace Clima.Basics.Services.Communication
{
    public interface INetworkInstaller
    {
        void RegisterServices(IServiceProvider provider);
        void RegisterMessages(IMessageTypeProvider messageProvider);
        void RegisterHandlers(IServiceExecutor serviceExecutor, IServiceProvider provider);
    }
}