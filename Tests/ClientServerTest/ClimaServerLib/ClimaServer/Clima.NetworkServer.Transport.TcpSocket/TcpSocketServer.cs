using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Clima.NetworkServer.Transport.TcpSocket
{
    public class TcpSocketServer:IServer
    {
        private ConcurrentDictionary<string,IConnection> _connections;
        private TcpListener _listener;
        private IPEndPoint _endPoint;
        private bool _exitSignal;
        private Thread _listenerThread;
        private List<Task> _listenerTasks;
        private readonly TcpServerConfig _config;
        public TcpSocketServer(TcpServerConfig config)
        {
            _config = config;
            _connections = new ConcurrentDictionary<string, IConnection>();
            IPAddress hostAddress = IPAddress.Parse(_config.HsotName);
            _endPoint = new IPEndPoint(hostAddress, _config.Port);
            
            _listener = new TcpListener(_endPoint);
            _listenerTasks = new List<Task>();
            _listenerThread = new Thread(ListeningThread);
            
        }
        public void Start()
        {
            _listenerThread.Start();
        }

        public Task SendAsync(string connectionId, string data)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<MessageEventArgs> MessageReceived;
        public event EventHandler<MessageEventArgs> ClientConnected;
        public event EventHandler<MessageEventArgs> ClientDisconnected;

        public IConnection TryGetConnection(string connectionId) =>
            _connections.TryGetValue(connectionId, out var result) ? result : null;

        public IEnumerable<IConnection> Connections => _connections.Values.ToArray();
        public bool IsRunning { get; private set; }
        private void ListeningThread()
        {
            if(IsRunning)
                return;

            IsRunning = true;
            _listener.Start();
            _exitSignal = false;

            while (!_exitSignal)
            {
                ConnectionLooper();
            }

            IsRunning = false;
        }

        private void ConnectionLooper()
        {
            while (_listenerTasks.Count < _config.MaxClientConnections)
            {
                var AwaiterTask = Task.Run(async () =>
                {
                    ProcessConnectionFromClient(await _listener.AcceptTcpClientAsync());
                });
                _listenerTasks.Add(AwaiterTask);
                
            }
            int removeAtIndex = Task.WaitAny(_listenerTasks.ToArray(), _config.NetworkTimeout);
            if (removeAtIndex > 0)
            {
                _listenerTasks.RemoveAt(removeAtIndex);
            }
        }

        private void ProcessConnectionFromClient(TcpClient client)
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}