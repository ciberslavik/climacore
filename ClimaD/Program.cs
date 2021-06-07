using System;
using Castle.Windsor;
using Clima.Services.Communication;
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
            
            
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
