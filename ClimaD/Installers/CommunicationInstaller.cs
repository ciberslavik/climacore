using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Clima.GrpcServer;
using Clima.Services.Communication;

namespace ClimaD.Installers
{
    public class CommunicationInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IAppServer>().ImplementedBy<GrpcAppServer>().LifestyleSingleton());
        }
    }
}
