namespace Clima.Basics.Services.Communication
{
    public interface INetworkServiceRegistrator
    {
        void RegisterNetworkService<TService>() where TService : INetworkService;
    }
}