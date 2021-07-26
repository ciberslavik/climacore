using System;
using System.Threading;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Clima.Core;
using Clima.Core.Controllers.Ventilation;
using Clima.ServiceContainer.CastleWindsor.Installers;
using IServiceProvider = Clima.Basics.Services.IServiceProvider;

namespace Clima.ServiceContainer.CastleWindsor
{
    public class ApplicationBuilder
    {
        private IWindsorContainer _container;
        private IServiceProvider _serviceProvider;
        public ApplicationBuilder()
        {
            
        }

        public void Initialize()
        {
            _container = new WindsorContainer();
            _serviceProvider = new CastleServiceProvider(_container);
            _container.Register(Component.For<IServiceProvider>().Instance(_serviceProvider));
            
            _container.Install(new BasicsInstaller());
            _container.Install(new CoreServicesInstaller());

            
            
            ClimaContext.InitContext(_serviceProvider);
            ClimaContext.ExitSignal = false;
            
            var fanFactory = _container.Resolve<IFanFactory>();

            var fan = fanFactory.GetAnalogFan(0);
            
            fan.Start();
        }

        public void Run()
        {
            while (!ClimaContext.ExitSignal)
            {
                Thread.Sleep(500);
            }
        }
    }
}