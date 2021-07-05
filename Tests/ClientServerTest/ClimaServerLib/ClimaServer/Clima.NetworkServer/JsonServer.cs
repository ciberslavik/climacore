using System;
using System.Threading.Tasks;
using Clima.NetworkServer.Exceptions;
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
        private readonly IMessageTypeProvider _messageTypeProvider;
        private readonly IServiceExecutor _executor;

        public bool IsDisposed { get; private set; }

        public IServer Server => _server;
        
        public JsonServer(IServer server, INetworkSerializer serializer,IMessageTypeProvider messageTypeProvider,IServiceExecutor executor)
        {
            _server = server ?? throw new ArgumentNullException(nameof(server));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            _messageTypeProvider = messageTypeProvider;
            _executor = executor;
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

                request = (RequestMessage) _serializer.Deserialize(e.Data, _messageTypeProvider, null);
                context.RequestMessage = request;
                try
                {
                    Console.WriteLine($"Execute service:{request.Name}");

                    var result = _executor.Execute(request.Name, request.Parameters);
                    if (result is Task task)
                    {
                        await task;
                        result = null;

                        var taskType = task.GetType();
                        if (taskType.IsGenericType)
                        {
                            var resultProperty = taskType.GetProperty(nameof(Task<bool>.Result));
                            result = resultProperty.GetValue(task);
                        }
                    }
                    response = new ResponseResultMessage()
                    {
                        Id = request.Id,
                        Result = result
                    };
                }
                catch (JsonServicesException exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
            finally
            {
                context.ResponseMessage = response;
                if (request == null || !request.IsNotification)
                {
                    try
                    {
                        var data = _serializer.Serialize(response);
                        await _server.SendAsync(e.ConnectionId, data);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                        throw;
                    }
                }
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