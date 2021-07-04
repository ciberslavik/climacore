using System;
using Clima.NetworkServer.Messages;
using Clima.NetworkServer.Serialization;
using Clima.NetworkServer.Services;
using Clima.NetworkServer.Transport;

namespace Clima.NetworkServer
{
    public class JsonServer:IDisposable
    {
        private readonly IServer _server;
        private readonly INetworkSerializer _serializer;

        public bool IsDisposed { get; private set; }

        public IServer Server => _server;
        
        public JsonServer(IServer server, INetworkSerializer serializer)
        {
            _server = server ?? throw new ArgumentNullException(nameof(server));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            _server.MessageReceived+= HandleServerMessage;
        }

        private async void HandleServerMessage(object? sender, MessageEventArgs e)
        {
            var request = default(RequestMessage);
            var response = default(ResponseMessage);
            var context = new RequestContext
            {
                Server = this,
                ConnectionId = e.ConnectionId,
            };
            try
            {
                RequestContext.CurrentContextHolder.Value = context;
                
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
        
        public event EventHandler<MessageEventArgs> ClientConnected
        {
            add { _server.ClientConnected += value; }
            remove{ _server.ClientConnected -= value; }
        }
        public event EventHandler<MessageEventArgs> ClientDisconnected
        {
            add { _server.ClientDisconnected += value; }
            remove{ _server.ClientDisconnected -= value; }
        }
        
        public void Dispose()
        {
            if (!IsDisposed)
            {
                _server.MessageReceived -= HandleServerMessage;
                _server.Dispose();
                IsDisposed = true;
            }
        }
    }
}