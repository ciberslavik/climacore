using Clima.NetworkServer.Sessions;

namespace Clima.NetworkServer.Transport
{
    public interface IConnection
    {
        string ConnectionId { get; }
        Session Session { get; set; }
    }
}