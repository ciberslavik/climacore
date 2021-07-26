using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Clima.AgavaModBusIO;
using Clima.Core.Conrollers.Ventilation.Ventilation;
using Clima.Core.Controllers.Ventilation;
using Clima.Core.Devices;
using Clima.Core.IO;

namespace Clima.ServiceContainer.CastleWindsor.Installers
{
    public class CoreServicesInstaller:IWindsorInstaller
    {
        public CoreServicesInstaller()
        {
        }


        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component
                    .For<IIOService>()
                    .ImplementedBy<AgavaIoService>()
                    .LifestyleSingleton(),
                Component
                    .For<IDeviceProvider>()
                    .ImplementedBy<CoreDeviceProvider>()
                    .LifestyleSingleton(),
                Component
                    .For<IFanFactory>()
                    .ImplementedBy<FanFactory>()
                    .LifestyleSingleton(),
                Component
                    .For<IVentilationController>()
                    .ImplementedBy<VentilationController>()
                    .LifestyleSingleton());


            var ioService = container.Resolve<IIOService>();
            
            ioService.Init();
            ioService.Start();
        }
    }
}