using System;
using System.Linq;
using Castle.Windsor;
using Clima.LocalDataBase.Security;
using Clima.Services.Communication;
using Clima.Services.Configuration;
using ClimaD.Installers;

namespace ClimaD
{
    class Program
    {
        static void Main(string[] args)
        {
            SecurityProvider provider = new SecurityProvider();

            provider.Users.Add(new User("Admin"));
            provider.SaveChanges();
            
            var user = provider.Users.OrderBy(b => b.UserId).First();
            Console.WriteLine($"User Name: {user.UserLogin}");
            
            IWindsorContainer _container = new WindsorContainer();
            _container.Install(new CommunicationInstaller());
            _container.Install(new IOInstaller());
            _container.Install(new ServicesInstaller());
            
            IAppServer server = _container.Resolve<IAppServer>();
            server.Start();

            var store = _container.Resolve<IConfigurationStorage>();
            
            var testConf = new TestConfig();
            testConf.Data = "Hello";
            store.RegisterConfig(testConf);

            var testConf2 = store.GetConfig<TestConfig>();
            Console.WriteLine(testConf2.Data);
            
            Console.WriteLine("Hello World!");
            Console.ReadKey();
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
