using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Clima.Authorization;
using Clima.NewtonSoftJsonSerializer;
using Clima.Services;
using Clima.Services.Authorization;
using Clima.Services.Configuration;
using FakeAuthorizationRepository;

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
                    .LifestyleSingleton(),
                Component
                    .For<IAuthorizationRepository>()
                    .ImplementedBy<AuthorizationRepository>()//Fake authorization repository
                    .LifestyleSingleton(),
                Component
                    .For<IWindsorContainer>()
                    .Instance(container)
                    .LifestyleSingleton(),
                Component
                    .For<IServiceProvider>()
                    .ImplementedBy<CastleServiceProvider>()
                    .LifestyleSingleton(),
                Component
                    .For<IAuthorizationService>()
                    .ImplementedBy<DefaultAuthorizationService>()
                    .LifestyleSingleton()
                );
        }
    }
}