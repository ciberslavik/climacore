using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Clima.AgavaModBusIO;
using Clima.Core.Conrollers.Ventilation;
using Clima.Core.Controllers.Ventilation;
using Clima.Core.Devices;
using Clima.Core.IO;
using Clima.Core.Tests;
using Clima.Core.Tests.IOService;

namespace Clima.ServiceContainer.CastleWindsor.Installers
{
    public class CoreServicesInstaller:IWindsorInstaller
    {
        private bool _isStub;
        public CoreServicesInstaller(bool stub)
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
                    .LifestyleSingleton());

            if (_isStub)
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
            }

            var ioService = container.Resolve<IIOService>();
            
            ioService.Init();
            ioService.Start();
        }
    }
}