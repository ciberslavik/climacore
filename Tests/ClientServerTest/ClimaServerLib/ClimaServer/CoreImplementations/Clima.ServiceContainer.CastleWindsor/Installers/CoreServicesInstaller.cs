using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Clima.AgavaModBusIO;
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
                    .LifestyleSingleton());


            var ioService = container.Resolve<IIOService>();
            
            ioService.Init();
            ioService.Start();
        }
    }
}