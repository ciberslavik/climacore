using System;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Clima.Bootstrapper.Installers;
using Clima.UI.Interface.Themes;
using Clima.UI.Interface.ViewModels;
using Clima.UI.Interface.Views;

namespace Clima.Bootstrapper
{
    public class ClimaBootstrapper
    {
        private IWindsorContainer _container;
        public void Initialize()
        {
            _container = new WindsorContainer();
            _container.Register(
                Component
                    .For<IWindsorContainer>()
                    .Instance(_container));

            _container.Kernel.Resolver.AddSubResolver(new ArrayResolver(_container.Kernel));
            _container.Install(new MvvmInstaller());

            

           // var themeManager = _container.Resolve<IThemeService>();
           // themeManager.LoadTheme(themeManager.InstalledThemes.FirstOrDefault());
            
            var win = _container.Resolve<IMainWindowView>();
            win.Show();
        }

        public void Run()
        {

        }
    }
}
