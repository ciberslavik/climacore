using System;
using Castle.Windsor;
using Clima.Services.Communication;
using Clima.Services.Configuration;
using ClimaD.Installers;

namespace ClimaD
{
    class Program
    {
        static void Main(string[] args)
        {
            IWindsorContainer _container = new WindsorContainer();
            _container.Install(new CommunicationInstaller());
            _container.Install(new IOInstaller());
            _container.Install(new ServicesInstaller());
            
            IAppServer server = _container.Resolve<IAppServer>();
            server.Start();

            var store = _container.Resolve<IConfigurationStorage>();
            store.RegisterConfig<TestConfig>();
            
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }

    public class TestConfig : ConfigItemBase
    {
        public TestConfig()
        {
            
        }
    }
}
