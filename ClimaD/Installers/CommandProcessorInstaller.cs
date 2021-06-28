using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Clima.CommandProcessor;
using Clima.CommandProcessor.ServiceProcessors;
using Clima.Services.Communication;

namespace ClimaD.Installers
{
    public class CommandProcessorInstaller:IWindsorInstaller
    {
        public CommandProcessorInstaller()
        {
        }


        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component
                                    .For<ICommandProcessor>()
                                    .ImplementedBy<CommandProcessor>()
                                    .LifestyleSingleton(),
                                Classes
                                    .FromAssemblyContaining(typeof(IServiceProcessor))
                                    .BasedOn<IServiceProcessor>()
                                    .WithServiceFromInterface()
                                    .LifestyleTransient());

            var server = container.Resolve<IServer>();
            var processor = container.Resolve<ICommandProcessor>();

            server.DataReceived += (ea) =>
            {
                processor.ProcessCommand(ea.Session, ea.Data);
            };
        }
    }
}