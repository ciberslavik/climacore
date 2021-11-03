using Castle.MicroKernel;
using Castle.Windsor;
using Clima.UI.Interface;
using Clima.UI.Interface.Views;

namespace Clima.Bootstrapper.MVVM
{
    public class WindsorViewFactory:IViewFactory
    {
        private readonly IWindsorContainer _container;

        public WindsorViewFactory(IWindsorContainer container)
        {
            _container = container;
        }
        public T CreateView<T>() where T : IView
        {
            return _container.Resolve<T>();
        }

        public T CreateView<T>(object argumentsAsAnonymousType) where T : IView
        {
            Arguments a = new Arguments();
            a.Add("", argumentsAsAnonymousType);

            return _container.Resolve<T>(a);
        }
    }
}