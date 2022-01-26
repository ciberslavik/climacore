using System;
using System.Threading;
using System.Threading.Tasks;
using Clima.Basics.Services;
using Clima.Basics.Services.Communication;
using Clima.Basics.Services.Communication.Exceptions;
using Clima.Basics.Services.Communication.Messages;
using Clima.NetworkServer.Services;
using Clima.NetworkServer.Sessions;
using Clima.NetworkServer.Transport;

namespace Clima.NetworkServer
{
    public class JsonServer : IDisposable, IJsonServer
    {
        private readonly IServer _server;
        private readonly INetworkSerializer _serializer;
        private readonly IMessageTypeProvider _messageTypeProvider;
        private readonly IServiceExecutor _executor;
        private readonly ISessionManager _sessionManager;
        public event EventHandler<ThreadExceptionEventArgs> UnhandledException;
        public ISystemLogger Logger { get; set; }
        public bool IsDisposed { get; private set; }
        private IExceptionTranslator ExceptionTranslator { get; }
        public IServer Server => _server;
        public object SessionManager => _sessionManager;

        public JsonServer(
            IServer server,
            INetworkSerializer serializer,
            IMessageTypeProvider messageTypeProvider,
            IServiceExecutor executor,
            ISessionManager sessionManager = null,
            IExceptionTranslator exceptionTranslator = null)
        {
            _server = server ?? throw new ArgumentNullException(nameof(server));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            _messageTypeProvider = messageTypeProvider ?? throw new ArgumentNullException(nameof(messageTypeProvider));
            _executor = executor ?? throw new ArgumentNullException(nameof(executor));
            _sessionManager = sessionManager ?? new SessionManagerDefault();
            _server.MessageReceived += HandleServerMessage;
            ExceptionTranslator = exceptionTranslator ?? new ExceptionTranslator();
        }

        private async void HandleServerMessage(object sender, MessageEventArgs e)
        {
            var request = default(RequestMessage);
            var response = default(ResponseMessage);
            var context = new RequestContext
            {
                Server = this,
                ConnectionId = e.ConnectionId
            };

            try
            {
                RequestContext.CurrentContextHolder.Value = context;
                //Logger.Debug($"HandleMessage:{e.Data}");
                request = (RequestMessage) _serializer.Deserialize(e.Data, _messageTypeProvider, null);
                context.RequestMessage = request;
                try
                {
                    //Logger.Debug($"Execute service:{request.Service}");

                    var result = _executor.Execute(request.Service, request.Method, request.Parameters);
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
                        Service = request.Service,
                        MethodName = request.Method,
                        Result = result
                    };
                }
                catch (JsonServicesException exception)
                {
                    response = new ResponseErrorMessage
                    {
                        Id = request.Id,
                        Error = ExceptionTranslator.Translate(exception)
                    };
                }
                catch (Exception exception)
                {
                    // error executing the service
                    response = new ResponseErrorMessage
                    {
                        Id = request.Id,
                        Error = ExceptionTranslator.Translate(exception,
                            InternalErrorException.ErrorCode,
                            "Internal server error: " + exception.Message)
                    };
                }
            }
            catch (JsonServicesException ex)
            {
                // report known error code
                response = new ResponseErrorMessage
                {
                    Id = ex.MessageId,
                    Error = ExceptionTranslator.Translate(ex)
                };
            }
            catch (Exception ex)
            {
                // deserialization error
                response = new ResponseErrorMessage
                {
                    Error = ExceptionTranslator.Translate(ex,
                        ParseErrorException.ErrorCode,
                        "Parse error: " + ex.Message)
                };
            }
            finally
            {
                context.ResponseMessage = response;
                if (request == null || !request.IsNotification)
                    try
                    {
                        var data = _serializer.Serialize(response);
                        e.Result = data;
                        //await _server.SendAsync(e.ConnectionId, data);
                    }
                    catch (Exception exception)
                    {
                        // report exceptions
                        var eargs = new ThreadExceptionEventArgs(exception);
                        UnhandledException?.Invoke(this, eargs);
                    }
            }
        }

        public event EventHandler<MessageEventArgs> ClientConnected
        {
            add => _server.ClientConnected += value;
            remove => _server.ClientConnected -= value;
        }

        public event EventHandler<MessageEventArgs> ClientDisconnected
        {
            add => _server.ClientDisconnected += value;
            remove => _server.ClientDisconnected -= value;
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