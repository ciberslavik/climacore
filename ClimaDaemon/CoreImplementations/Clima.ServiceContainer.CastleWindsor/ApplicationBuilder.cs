using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Clima.AgavaModBusIO;
using Clima.AgavaModBusIO.Configuration;
using Clima.Basics;
using Clima.Basics.Configuration;
using Clima.Basics.Services;
using Clima.Core;
using Clima.Core.Controllers.Ventilation;
using Clima.Core.Devices;
using Clima.Core.IO;
using Clima.Core.Scheduler;
using Clima.Core.Tests.IOService;
using Clima.Core.Tests.IOService.Configurations;
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
       
        private ISystemLogger _logger;
        private const bool isDebug = true;
        public ApplicationBuilder()
        {
        }

        public void Initialize()
        {
            _container = new WindsorContainer();
            //Register logger
            _container.Register(Component.For<ISystemLogger>().ImplementedBy<ConsoleSystemLogger>());
            _logger = _container.Resolve<ISystemLogger>();
            ClimaContext.Logger = _logger;
            _container.Install(new BasicsInstaller(_logger));

            //if parameter is true then stub io service else real io service
            _container.Install(new CoreServicesInstaller(true, _logger));
            _container.Install(new SchedulerInstaller(_logger));
            _serviceProvider = new CastleServiceProvider(_container);
            _container.Register(Component.For<IServiceProvider>().Instance(_serviceProvider));
            

            _container.Install(new NetworkInstaller(_logger));
            
            _logger.Info("Register core services");
            //register core services
            _container.Register(
                Classes
                    .FromAssemblyInDirectory(new AssemblyFilter(Environment.CurrentDirectory))
                    .BasedOn<IService>()
                    .WithServiceAllInterfaces()
                    .WithServiceBase()
                    .LifestyleSingleton());
            
            
            InitializeCoreServices();
            ClimaContext.InitContext(_serviceProvider);
            StartCoreServices();

            var server = _container.Resolve<IJsonServer>();
        }

        private void InitializeCoreServices()
        {
            _logger.Info("Initialize core services");
            
            CreateIOService();
            
            var services = _container.ResolveAll<IService>();
            var configStore = _container.Resolve<IConfigurationStorage>();
            
            var t = typeof(IConfigurationStorage);
            MethodInfo getConfigMi = t.GetMethod(nameof(IConfigurationStorage.GetConfig), Type.EmptyTypes);
            if (getConfigMi is not null)
            {
                foreach (var service in services)
                {
                    if (service.ConfigType is null)
                        throw new ArgumentNullException($"{service.GetType().Name}.ConfigType", $"Service:{service.GetType().Name} ConfigType is null");
                    
                    var serviceConfig = getConfigMi.MakeGenericMethod(service.ConfigType)
                        .Invoke(configStore, new object[] { });

                    _logger.Debug($"Initialize :{service.GetType().Name}");

                    service.Init(serviceConfig);
                }
            }

            
        }

        private void CreateIOService()
        {
            var configStore = _container.Resolve<IConfigurationStorage>();
            IConfigurationItem ioconfig = default;
            if (isDebug)
            {
                _container.Register(Component.For<IIOService>().ImplementedBy<StubIOService>().LifestyleSingleton());
                ioconfig = configStore.GetConfig<StubIOServiceConfig>();
            }
            else
            {
                _container.Register(Component.For<IIOService>().ImplementedBy<AgavaIoService>().LifestyleSingleton());
                ioconfig = configStore.GetConfig<ModbusConfig>();
            }

            if (ioconfig is not null)
            {
                _container.Resolve<IIOService>().Init(ioconfig);
            }
            configStore.Save();
        }
        private void StartCoreServices()
        {
            _logger.Info("Starting core services");
            _container.Resolve<IIOService>().Start();
            
            var services = _container.ResolveAll<IService>();
            foreach (var service in services)
            {

                service.Start();
            }
            
            
        }
    }
}