namespace Clima.Services
{
    public interface IServiceProvider
    {
        T Resolve<T>();
    }
}