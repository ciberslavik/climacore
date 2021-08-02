using System;
using System.Net.Mime;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Clima.Basics.Services.Communication;
using Clima.Basics.Services.Communication.Messages;
using Clima.Communication;
using Clima.Communication.Messages;
using Clima.NetworkServer;
using Clima.NetworkServer.Serialization.Newtonsoft;
using Clima.NetworkServer.Services;
using Clima.NetworkServer.Transport;
using Clima.NetworkServer.Transport.TcpSocket;
using IServiceProvider = Clima.Basics.Services.IServiceProvider;

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
            var serverConfig = TcpServerConfig.CreateDefault("", 5911);
            var server = new TcpSocketServer(serverConfig);

            
            container.Register(
                Component
                    .For<IServer>()
                    .Instance(server)
                    .LifestyleSingleton());

            server.Start();


            container.Register(
                Classes
                    .FromAssemblyInDirectory(new AssemblyFilter(Environment.CurrentDirectory))
                    .BasedOn<INetworkInstaller>()
                    .WithServiceBase());
            
            
            var messageTypeProvider = container.Resolve<IMessageTypeProvider>();
            var serviceExecutor = container.Resolve<IServiceExecutor>();
            var serviceProvider = container.Resolve<IServiceProvider>();
            
            var installers = container.ResolveAll<INetworkInstaller>();
            foreach (var instller in installers)
            {
                instller.RegisterServices(serviceProvider);
                instller.RegisterMessages(messageTypeProvider);
                instller.RegisterHandlers(serviceExecutor, serviceProvider);
            }
            
        }
    }
}