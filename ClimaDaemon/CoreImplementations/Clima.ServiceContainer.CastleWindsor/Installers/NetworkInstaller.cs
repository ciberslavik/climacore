using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Clima.Basics.Services.Communication;
using Clima.Basics.Services.Communication.Messages;
using Clima.Communication;
using Clima.Communication.Messages;
using Clima.Communication.Services;
using Clima.NetworkServer;
using Clima.NetworkServer.Serialization.Newtonsoft;
using Clima.NetworkServer.Services;
using Clima.NetworkServer.Transport;
using Clima.NetworkServer.Transport.TcpSocket;

namespace Clima.ServiceContainer.CastleWindsor.Installers
{
    public class NetworkInstaller:IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component
                    .For<IMessageTypeProvider>()
                    .ImplementedBy<MessageTypeProvider>()
                    .LifestyleSingleton(),
                Component
                    .For<IMessageNameProvider>()
                    .ImplementedBy<MessageNameProvider>()
                    .LifestyleSingleton(),
                Component
                    .For<IServiceExecutor>()
                    .ImplementedBy<ServiceExecutor>()
                    .LifestyleSingleton(),
                Component
                    .For<IJsonServer>()
                    .ImplementedBy<JsonServer>()
                    .LifestyleSingleton());
            
            //Create instance of TCP Server by default configuration

            var server = new TcpSocketServer(TcpServerConfig.CreateDefault());

            container.Register(
                Component
                    .For<IServer>()
                    .Instance(server)
                    .LifestyleSingleton());

            server.Start();

            var messageTypeProvider = container.Resolve<IMessageTypeProvider>();
            //Clima.Communication.Messages.ServerInfoRequest
            messageTypeProvider.Register(typeof(ServerInfoService).FullName, typeof(ServerInfoRequest),
                typeof(ServerInfoResponse));
            
        }
    }
}