using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Clima.Basics.Services.Communication;
using Clima.Basics.Services.Communication.Messages;
using Clima.Communication;
using Clima.NetworkServer;
using Clima.NetworkServer.Transport;
using Clima.NetworkServer.Transport.TcpSocket;

namespace Clima.ServiceContainer.CastleWindsor.Installers
{
    public class NetworkInstaller : IWindsorInstaller
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
                    .LifestyleSingleton(),
                Component
                    .For<INetworkServiceRegistrator>()
                    .ImplementedBy<DefaultServiceRegistrator>()
                    .LifestyleTransient());

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

            var serviceRegistrator = container.Resolve<INetworkServiceRegistrator>();

            //GraphProviderService serv = new GraphProviderService(serviceProvider.Resolve<Te>());
            var installers = container.ResolveAll<INetworkInstaller>();

            foreach (var installer in installers) installer.InstallServices(serviceRegistrator);
            Console.WriteLine("Network services initialized...");
        }
    }
}