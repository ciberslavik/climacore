using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Clima.NewtonSoftJsonSerializer;
using Clima.Services;
using Clima.Services.Configuration;

namespace ClimaD.Installers
{
    public class ServicesInstaller:IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component
                    .For<IFileSystem>()
                    .ImplementedBy<DefaultFileSystem>()
                    .LifestyleSingleton(), 
                Component
                    .For<IConfigurationSerializer>()
                    .ImplementedBy<NewtonsoftConfigSerializer>()
                    .LifestyleTransient(),
                Component
                    .For<IConfigurationStorage>()
                    .ImplementedBy<DefaultConfigurationStorage>()
                    .LifestyleSingleton());
        }
    }
}