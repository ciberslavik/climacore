using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Clima.Basics.Configuration;
using Clima.Basics.Services;
using Clima.Basics.Services.Communication;
using Clima.Configuration.FileSystem;
using Clima.NetworkServer.Serialization.Newtonsoft;
using Clima.Serialization.Newtonsoft;

namespace Clima.ServiceContainer.CastleWindsor.Installers
{
    public class BasicsInstaller:IWindsorInstaller
    {
        public BasicsInstaller(ISystemLogger logger)
        {
            Log = logger;
        }
        public ISystemLogger Log { get; private set; }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            Log.Info("Install basic components");
            container.Register(
                Component
                    .For<IConfigurationSerializer>()
                    .ImplementedBy<ConfigurationSerializer>()
                    .LifestyleSingleton(),
                Component
                    .For<INetworkSerializer>()
                    .ImplementedBy<JsonNetworkSerializer>()
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