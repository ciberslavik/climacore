using System;
using System.Linq;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Clima.DataModel.Configurations;
using Clima.LocalDataBase.Security;
using Clima.Services.Communication;
using Clima.Services.Configuration;
using Clima.TcpServer;
using ClimaD.Installers;

namespace ClimaD
{
    class Program
    {
        static void Main(string[] args)
        {
            
            IWindsorContainer _container = new WindsorContainer();
            //
            _container.Install(new ServicesInstaller());
            _container.Install(new IOInstaller());
            _container.Install(new CommunicationInstaller());
            _container.Install(new CommandProcessorInstaller());
            _container.Install(new CoreInstaller());
            
            //IAppServer server = _container.Resolve<IAppServer>();
            //server.Start();

            var store = _container.Resolve<IConfigurationStorage>();
            
            var testConf = new TestConfig();
            testConf.Data = "Hello";
            store.RegisterConfig(testConf);

            var testConf2 = store.GetConfig<TestConfig>();
            Console.WriteLine(testConf2.Data);
            
            Console.WriteLine("Server starting...");

            var server = _container.Resolve<IServer>();
            
            server.StartServer();
            Console.WriteLine("Server started...");
            Console.WriteLine("Press esc key to stop");
            
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                Console.Clear();
                Console.WriteLine("Press esc key to stop");
            }

            Console.WriteLine("Attempting clean exit");
            server.WaitStopServer();

            Console.WriteLine("Exiting console Main.");
            
            
        }
    }

    public class TestConfig : ConfigItemBase
    {
        private string _data;
        public TestConfig()
        {
            Data = "";
        }

        public string Data
        {
            get => _data;
            set => _data = value;
        }
    }
}
