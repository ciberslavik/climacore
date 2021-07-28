using System;
using System.Threading;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Clima.Basics.Services;
using Clima.Core;
using Clima.Core.Controllers.Ventilation;
using Clima.Core.Devices;
using Clima.Logger.Console;
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
            //Register logger
            _container.Register(Component.For<ISystemLogger>().ImplementedBy<ConsoleSystemLogger>());
            
            
            _container.Install(new BasicsInstaller());
            _container.Install(new CoreServicesInstaller());

            _serviceProvider = new CastleServiceProvider(_container);
            _container.Register(Component.For<IServiceProvider>().Instance(_serviceProvider));

            
            ClimaContext.InitContext(_serviceProvider);
            ClimaContext.ExitSignal = false;


            var devProvider = _container.Resolve<IDeviceProvider>();
            
            var relay = devProvider.GetRelay("REL:0");
            
            var relayNotifier = relay as IAlarmNotifier;
            if (relayNotifier != null)
            {
                relayNotifier.AlarmNotify += (s,ea) =>
                {
                    ClimaContext.Current.Logger.System($"Sender:{s.GetType().FullName}");
                    ClimaContext.Current.Logger.System($"Alarm:{ea.Message}");
                };
            }

            var servo = devProvider.GetServo("SERVO:0");
            Thread.Yield();
            servo.SetPosition(40);
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