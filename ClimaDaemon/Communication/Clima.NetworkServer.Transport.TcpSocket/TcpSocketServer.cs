using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Clima.Basics.Services;

namespace Clima.NetworkServer.Transport.TcpSocket
{
    public class TcpSocketServer : IServer
    {
        private ConcurrentDictionary<string, IConnection> _connections;
        private TcpListener _listener;
        private IPEndPoint _endPoint;
        private bool _exitSignal;
        private Thread _listenerThread;
        private List<Task> _listenerTasks;
        private TcpServerConfig _config;

        public TcpSocketServer()
        {
            _connections = new ConcurrentDictionary<string, IConnection>();
            _listenerTasks = new List<Task>();
            _listenerThread = new Thread(ListeningThread);
        }
        public ISystemLogger Log { get; set; }
        public void Start()
        {
            _listenerThread.Start();
        }

        public void Stop()
        {
            _exitSignal = true;
            _listenerThread.Join();
            IsRunning = false;
        }

        public void Init(object config)
        {
            if (config is TcpServerConfig cfg)
                _config = cfg;
            else
                throw new ArgumentException($"Configuration is not a {nameof(TcpServerConfig)} type", nameof(config));
            
            Log.Info("Start initialize tcp server");
            IPAddress hostAddress;
            if (_config.HsotName == "")
                hostAddress = IPAddress.Any;
            else
                hostAddress = IPAddress.Parse(_config.HsotName);

            _endPoint = new IPEndPoint(hostAddress, _config.Port);

            _listener = new TcpListener(_endPoint);
        }

        public Type ConfigType => typeof(TcpServerConfig);
        public ServiceState ServiceState { get; }

        public Task SendAsync(string connectionId, string data)
        {
            var connection = TryGetConnection(connectionId);
            if (connection != null)
                return Task.Run(async () =>
                {
                    var tcpSession = (TcpSocketSession) connection;
                    await Task.Run(() => tcpSession.SendStringAsync(data));
                });

            return null;
        }

        public event EventHandler<MessageEventArgs> MessageReceived;
        public event EventHandler<MessageEventArgs> ClientConnected;
        public event EventHandler<MessageEventArgs> ClientDisconnected;

        public IConnection TryGetConnection(string connectionId)
        {
            return _connections.TryGetValue(connectionId, out var result) ? result : null;
        }

        public IEnumerable<IConnection> Connections => _connections.Values.ToArray();
        public bool IsRunning { get; private set; }

        private void ListeningThread()
        {
            if (IsRunning)
                return;

            IsRunning = true;
            _listener.Start();
            _exitSignal = false;

            while (!_exitSignal)
            {
                ConnectionLooper();
                Thread.Yield();
            }
            IsRunning = false;
        }

        private void ConnectionLooper()
        {
            while (_listenerTasks.Count < _config.MaxClientConnections)
            {
                var AwaiterTask = Task.Run(async () =>
                {
                    Console.WriteLine($"Listening on thread:{Thread.CurrentThread.ManagedThreadId}");
                    ProcessConnectionFromClient(await _listener.AcceptTcpClientAsync());
                });
                _listenerTasks.Add(AwaiterTask);
            }

            var removeAtIndex = Task.WaitAny(_listenerTasks.ToArray(), _config.NetworkTimeout);
            
            if (removeAtIndex > 0) 
                _listenerTasks.RemoveAt(removeAtIndex);
        }

        private void ProcessConnectionFromClient(TcpClient client)
        {
            if (client.Connected)
            {
                Console.WriteLine($"Client connection established {client.Client.RemoteEndPoint}");
                var session = new TcpSocketSession();
                session.ReceiveBufferSize = _config.ReceiveBufferSize;
                session.SendBufferSize = _config.SendBufferSize;
                session.MessageReceived += SessionOnMessageReceived;
                session.Disconnected += SessionOnDisconnected;
                try
                {
                    session.Connect(client.Client);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }

                _connections.TryAdd(session.ConnectionId, session);

                ClientConnected?.Invoke(this, new MessageEventArgs()
                {
                    ConnectionId = session.ConnectionId
                });
            }
        }

        private void SessionOnDisconnected(object? sender, EventArgs e)
        {
            if (sender is TcpSocketSession session)
            {
                Console.WriteLine($"Disconnected:{session.ConnectionId}");
                _connections.TryRemove(session.ConnectionId, out _);
                session.Disconnect();
                
            }
        }

        private void SessionOnMessageReceived(object sender, MessageEventArgs e)
        {
            MessageReceived?.Invoke(sender, e);
        }


        public void Dispose()
        {
        }
    }
}