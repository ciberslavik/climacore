using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Clima.Basics.Services;
using Clima.Core.Scheduler;

namespace Clima.ServiceContainer.CastleWindsor.Installers
{
    public class SchedulerInstaller:IWindsorInstaller
    {
        private readonly ISystemLogger _logger;
        public SchedulerInstaller(ISystemLogger logger)
        {
            _logger = logger;
        }
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            _logger.Info("Install scheduler components");
            container.Register(Component.For<IClimaScheduler>().ImplementedBy<ClimaScheduler>());
        }
    }
}