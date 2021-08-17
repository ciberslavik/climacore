using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Clima.AgavaModBusIO;
using Clima.Basics.Services;
using Clima.Core.Conrollers.Ventilation;
using Clima.Core.Controllers.Ventilation;
using Clima.Core.DataModel.GraphModel;
using Clima.Core.Devices;
using Clima.Core.IO;
using Clima.Core.Tests;
using Clima.Core.Tests.IOService;
using Clima.FSGrapRepository;

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
                    .For<IVentilationController>()
                    .ImplementedBy<VentilationController>()
                    .LifestyleSingleton(),
                Component
                    .For<IGraphProviderFactory>()
                    .ImplementedBy<GraphProviderFactoryFileSystem>()
                    .LifestyleSingleton());

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