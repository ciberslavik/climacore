using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Clima.Core;

namespace ClimaD.Installers
{
    public class CoreInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component
                                .For<IClimaSheduler>()
                                .ImplementedBy<ClimaSheduler>()
                                .LifestyleSingleton()
            );
        }
    }
}