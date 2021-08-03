using Clima.Basics.Services;
using Clima.Basics.Services.Communication;
using Clima.Basics.Services.Communication.Messages;

namespace Clima.Core.Sheduler.Network
{
    public class ShedulerNetworkInstaller:INetworkInstaller
    {
        public ShedulerNetworkInstaller()
        {
        }


        public void RegisterServices(IServiceProvider provider)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterMessages(IMessageTypeProvider messageProvider)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterHandlers(IServiceExecutor serviceExecutor, IServiceProvider provider)
        {
            throw new System.NotImplementedException();
        }
    }
}