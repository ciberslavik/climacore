namespace Clima.Basics.Services
{
    public interface IServiceProvider
    {
        T Resolve<T>();
    }
}