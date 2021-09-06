using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Clima.AgavaModBusIO;
using Clima.Basics.Services;
using Clima.Core;
using Clima.Core.Conrollers.Ventilation;
using Clima.Core.Controllers;
using Clima.Core.Controllers.Heater;
using Clima.Core.Controllers.Light;
using Clima.Core.Controllers.Ventilation;
using Clima.Core.DataModel.GraphModel;
using Clima.Core.Devices;
using Clima.Core.IO;
using Clima.Core.Tests;
using Clima.Core.Tests.IOService;
using Clima.FSGrapRepository;
using Clima.FSLightRepository;
using Clima.ServiceContainer.CastleWindsor.Factories;
using IIOServiceFactory = Clima.Core.IO.IIOServiceFactory;

namespace Clima.ServiceContainer.CastleWindsor.Installers
{
    public class CoreServicesInstaller : IWindsorInstaller
    {
        private readonly bool _isStub;

        public CoreServicesInstaller(bool stub, ISystemLogger logger)
        {
            _isStub = stub;
        }


        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component
                    .For<IDeviceProvider>()
                    .ImplementedBy<CoreDeviceProvider>()
                    .LifestyleSingleton(),
                Component
                    .For<IControllerFactory>()
                    .ImplementedBy<ControllerFactory>()
                    .LifestyleSingleton(),
                Component
                    .For<ILightControllerDataRepo>()
                    .ImplementedBy<FSLightControllerRepo>()
                    .LifestyleSingleton(),
                Component
                    .For<IGraphProviderFactory>()
                    .ImplementedBy<GraphProviderFactoryFileSystem>()
                    .LifestyleSingleton(),
                Component
                    .For<ITimeProvider>()
                    .ImplementedBy<DefaultTimeProvider>()
                    .LifestyleTransient(),
                Component
                    .For<IHeaterController>()
                    .ImplementedBy<HeaterController>()
                    .LifestyleSingleton());
            
            container.AddFacility<TypedFactoryFacility>();
            
            container.Register(
                Component
                    .For<ITypedFactoryComponentSelector>()
                    .ImplementedBy<IOServiceSelector>(),
                Component
                    .For<IIOServiceFactory>()
                    .AsFactory(c=>c.SelectedWith<IOServiceSelector>()));
            
            /*
                Component.For<IOServiceSelector>(),
                Component
                    .For<IIOServiceFactory>()
                    .AsFactory(new IOServiceSelector())
                );*/
            /*if (_isStub)
            {
                container.Register(
                    Component
                        .For<IIOService>()
                        .ImplementedBy<StubIOService>()
                        .LifestyleSingleton());
            }
            else
            {
                container.Register(
                    Component
                        .For<IIOService>()
                        .ImplementedBy<AgavaIoService>()
                        .LifestyleSingleton());
            }*/
        }
    }
}