using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clima.NetworkServer.Transport
{
    public interface IServer : IDisposable
    {
        void Start();
        void Stop();
        Task SendAsync(string connectionId, string data);
        event EventHandler<MessageEventArgs> MessageReceived;
        event EventHandler<MessageEventArgs> ClientConnected;
        event EventHandler<MessageEventArgs> ClientDisconnected;
        IConnection TryGetConnection(string connectionId);
        IEnumerable<IConnection> Connections { get; }
    }
}