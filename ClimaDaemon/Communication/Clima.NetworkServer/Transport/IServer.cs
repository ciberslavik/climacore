using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Clima.Basics.Services;

namespace Clima.NetworkServer.Transport
{
    public interface IServer :IService, IDisposable
    {
        Task SendAsync(string connectionId, string data);
        event EventHandler<MessageEventArgs> MessageReceived;
        event EventHandler<MessageEventArgs> ClientConnected;
        event EventHandler<MessageEventArgs> ClientDisconnected;
        IConnection TryGetConnection(string connectionId);
        IEnumerable<IConnection> Connections { get; }
    }
}