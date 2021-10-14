using System.IO;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Clima.Bootstrapper.MVVM;
using Clima.UI.Interface;
using Clima.UI.Interface.Themes;
using Clima.UI.Interface.ViewModels;
using Clima.UI.Interface.Views;

namespace Clima.Bootstrapper.Installers
{
    public class MvvmInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            string assemblyFile = (
                new System.Uri(Assembly.GetExecutingAssembly().CodeBase)
            ).AbsolutePath;

            string binDirectory = Path.GetDirectoryName(assemblyFile);
            AssemblyFilter asmFilter = new AssemblyFilter(binDirectory, "Clima.UI.*.dll");
            container.Register(
                Classes.FromAssemblyInDirectory(asmFilter)
                    .BasedOn<IViewModelAssignation>()
                    .WithServiceFromInterface()
                    .LifestyleTransient());

            container.AddFacility<ViewActivatorFacility>();

            container.Register(
                Classes.FromAssemblyInDirectory(asmFilter)
                    .BasedOn<IView>()
                    .WithServiceFromInterface()
                    .LifestyleTransient(),

                Classes.FromAssemblyInDirectory(asmFilter)
                    .BasedOn<IViewModel>()
                    .WithServiceFromInterface()
                    .LifestyleTransient()
                );

            container.Register(
                Classes.FromAssemblyInDirectory(asmFilter)
                    .BasedOn<IThemeService>()
                    .WithServiceFromInterface()
                    .LifestyleSingleton(),

                Classes.FromAssemblyInDirectory(asmFilter)
                    .BasedOn<Theme>()
                    .WithServiceBase()
                    .LifestyleSingleton()
                    .AllowMultipleMatches());
        }
    }
}