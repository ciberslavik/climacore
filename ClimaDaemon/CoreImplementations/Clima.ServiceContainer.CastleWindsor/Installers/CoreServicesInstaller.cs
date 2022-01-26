using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Clima.Basics.Services;
using Clima.Core;
using Clima.Core.DataModel.GraphModel;
using Clima.Core.Devices;
using Clima.FSGrapRepository;

namespace Clima.ServiceContainer.CastleWindsor.Installers
{
    public class CoreServicesInstaller : IWindsorInstaller
    {
        private readonly ISystemLogger _logger;

        public CoreServicesInstaller(ISystemLogger logger)
        {
            _logger = logger;
        }


        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component
                    .For<IDeviceProvider>()
                    .ImplementedBy<CoreDeviceProvider>()
                    .LifestyleSingleton(),
                //Component
                  //  .For<IControllerFactory>()
                    //.ImplementedBy<ControllerFactory>()
                  //  .LifestyleSingleton(),
                Component
                    .For<IGraphProviderFactory>()
                    .ImplementedBy<GraphProviderFactoryFileSystem>()
                    .LifestyleSingleton(),
                Component
                    .For<ITimeProvider>()
                    .ImplementedBy<DefaultTimeProvider>()
                    .LifestyleTransient());
            
            container.AddFacility<TypedFactoryFacility>();
            
        }
    }
}