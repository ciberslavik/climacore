using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Clima.Basics.Configuration;
using Clima.Basics.Services;
using Clima.Configuration.FileSystem;
using Clima.Serialization.Newtonsoft;

namespace Clima.ServiceContainer.CastleWindsor.Installers
{
    public class BasicsInstaller:IWindsorInstaller
    {
        public BasicsInstaller()
        {
        }


        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component
                    .For<IConfigurationSerializer>()
                    .ImplementedBy<ConfigurationSerializer>()
                    .LifestyleSingleton(),
                Component
                    .For<IFileSystem>()
                    .ImplementedBy<DefaultFileSystem>()
                    .LifestyleSingleton(),
                Component
                    .For<IConfigurationStorage>()
                    .ImplementedBy<FSConfigurationStorage>()
                    .LifestyleSingleton());
        }
    }
}