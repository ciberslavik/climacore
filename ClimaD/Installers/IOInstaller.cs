using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Clima.Services.IO;
using Clima.AgavaModBusIO;
using FakeIOService;

namespace ClimaD.Installers
{
    public class IOInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //container.Register(Component.For<IIOService>().ImplementedBy<AgavaIoService>().LifestyleSingleton());
            container.Register(Component.For<IIOService>().ImplementedBy<FakeIO>().LifestyleSingleton());
            var io = container.Resolve<IIOService>();

            try
            {
                //io.Init();
            }
            catch (IOServiceException e)
            {
                Console.WriteLine(e);
            }
            
            io.Start();
        }
    }
}
