using Clima.NetworkServer.Transport;

namespace Clima.NetworkServer
{
    public interface IJsonServer
    {
        bool IsDisposed { get; }
        IServer Server { get; }
        object SessionManager { get; }
    }
}