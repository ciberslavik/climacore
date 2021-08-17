using System;
using System.Reflection;
using System.Threading;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Clima.Basics;
using Clima.Basics.Configuration;
using Clima.Basics.Services;
using Clima.Core;
using Clima.Core.Controllers.Ventilation;
using Clima.Core.Devices;
using Clima.Core.Sheduler;
using Clima.Logger;
using Clima.NetworkServer;
using Clima.ServiceContainer.CastleWindsor.Installers;
using IServiceProvider = Clima.Basics.Services.IServiceProvider;

namespace Clima.ServiceContainer.CastleWindsor
{
    public class ApplicationBuilder
    {
        private IWindsorContainer _container;
        private IServiceProvider _serviceProvider;
        private IJsonServer _server;
        private IClimaSheduler _sheduler;
        private ISystemLogger _logger;
        public ApplicationBuilder()
        {
        }

        public void Initialize()
        {
            _container = new WindsorContainer();
            //Register logger
            _container.Register(Component.For<ISystemLogger>().ImplementedBy<ConsoleSystemLogger>());
            _logger = _container.Resolve<ISystemLogger>();
            _container.Install(new BasicsInstaller(_logger));

            //if parameter is true then stub io service else real io service
            _container.Install(new CoreServicesInstaller(true, _logger));
            _container.Install(new SchedulerInstaller(_logger));
            _serviceProvider = new CastleServiceProvider(_container);
            _container.Register(Component.For<IServiceProvider>().Instance(_serviceProvider));


            ClimaContext.InitContext(_serviceProvider);

            _container.Install(new NetworkInstaller(_logger));
            
            _logger.Info("Register core services");
            //register core services
            _container.Register(
                Classes
                    .FromAssemblyInDirectory(new AssemblyFilter(Environment.CurrentDirectory))
                    .BasedOn<IService>()
                    .WithServiceAllInterfaces()
                    .LifestyleSingleton());
            
            
            InitializeCoreSevices();
            
            StartCoreServices();
        }

        private void InitializeCoreSevices()
        {
            _logger.Info("Initialize core services");
            var services = _container.ResolveAll<IService>();
            var configStore = _container.Resolve<IConfigurationStorage>();
            var t = typeof(IConfigurationStorage);
            MethodInfo getConfigMi = t.GetMethod(nameof(IConfigurationStorage.GetConfig), Type.EmptyTypes);
            if (getConfigMi is not null)
            {
                foreach (var service in services)
                {

                    var serviceConfig = getConfigMi.MakeGenericMethod(service.ConfigType)
                        .Invoke(configStore, new object[] { });

                    service.Init(serviceConfig);
                }
            }
        }

        private void StartCoreServices()
        {
            _logger.Info("Starting core services");
            var services = _container.ResolveAll<IService>();
            foreach (var service in services)
            {
                service.Start();
            }
        }
        public async void ProcessSheduler()
        {
            _sheduler.Process();
        }
    }
}