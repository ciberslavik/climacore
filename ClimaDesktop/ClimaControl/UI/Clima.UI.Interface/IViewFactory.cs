using Clima.UI.Interface.Views;

namespace Clima.UI.Interface
{
    public interface IViewFactory
    {
        T CreateView<T>() where T : IView;
        T CreateView<T>(object argumentsAsAnonymousType) where T : IView;
    }
}