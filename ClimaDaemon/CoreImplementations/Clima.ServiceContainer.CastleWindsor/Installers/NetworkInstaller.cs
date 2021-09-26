using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Clima.Basics.Services;
using Clima.Basics.Services.Communication;
using Clima.Basics.Services.Communication.Messages;
using Clima.Communication;
using Clima.NetworkServer;

namespace Clima.ServiceContainer.CastleWindsor.Installers
{
    public class NetworkInstaller : IWindsorInstaller
    {
        private readonly ISystemLogger _logger;

        public NetworkInstaller(ISystemLogger logger)
        {
            _logger = logger;
        }
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            _logger.Info("Register network components");
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
            /*container.Register(
                Component
                    .For<IServer>()
                    .ImplementedBy<TcpSocketServer>()
                    .LifestyleSingleton());*/

            _logger.Info("Register network services");
            SearchAndRegisterServices(container);
            
            _logger.Info("Network services Registered...");
        }

        private void SearchAndRegisterServices(IWindsorContainer container)
        {
            container.Register(
                Classes
                    .FromAssemblyInDirectory(new AssemblyFilter(Environment.CurrentDirectory))
                    .BasedOn<INetworkInstaller>()
                    .WithServiceBase());

            var serviceRegistrator = container.Resolve<INetworkServiceRegistrator>();

            //GraphProviderService serv = new GraphProviderService(serviceProvider.Resolve<Te>());
            var installers = container.ResolveAll<INetworkInstaller>();

            foreach (var installer in installers) installer.InstallServices(serviceRegistrator);
        }
    }
}