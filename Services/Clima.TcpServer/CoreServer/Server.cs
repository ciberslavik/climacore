using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Clima.Services.Communication;

namespace Clima.TcpServer.CoreServer
{
    

    public class Server : IServer
    {
#region Events
        
        
        public event SessionCreatedEventHandler SessionCreated;
        protected virtual void OnSessionCreated(SessionCreatedEventArgs ea)
        {
            SessionCreated?.Invoke(this, ea);
        }

        public event DataReceivedEventHandler DataReceived;
        protected virtual void OnDataReceived(DataReceivedEventArgs ea)
        {
            DataReceived?.Invoke(ea);
        }
#endregion Events
#region Private Fields
        private const int MAX_LISTENER = 3;
        private TcpListener _listener;
        private Thread _listenerThread;
        private bool _exitSignal;
        private List<Task> _listenerTasks;
        private ConcurrentDictionary<Guid, Session> _sessions;
        private ServerConfig _config;
        #endregion Private Fields
        public bool IsRunning { get; private set; }
        
        public Server(ServerConfig config)
        {
            _config = config;
            
            _listenerTasks = new List<Task>();
            _sessions = new ConcurrentDictionary<Guid, Session>();
            
            _listener = new TcpListener(IPAddress.Parse(_config.Host), _config.Port);
            _listenerThread = new Thread(Run);
            _exitSignal = false;
        }

        public bool StartServer()
        {
            _listenerThread.Start();
            return true;
        }

        public void WaitStopServer()
        {
            Console.WriteLine("Stopping server...");
            _exitSignal = true;
            _listenerThread.Join();
            IsRunning = false;
        }

        
        public void SendBroadcast()
        {
            
        }
        private void Run()
        {
            if (IsRunning)
                return;
            IsRunning = true;
            _listener.Start();
            _exitSignal = false;

            while (!_exitSignal)
                ConnectionLooper();

            IsRunning = false;
        }

        internal ServerConfig Confoguration
        {
            get => _config;
            set => _config = value;
        }
        private void ConnectionLooper()
        {
            while (_listenerTasks.Count < MAX_LISTENER)
            {
                var AwaiterTask = Task.Run(async () =>
                {
                    Console.WriteLine($"Listening on thread:{Thread.CurrentThread.ManagedThreadId}");
                    ProcessConnectionFromClient(await _listener.AcceptTcpClientAsync());
                });
                _listenerTasks.Add(AwaiterTask);
            }
            int removaAtIndex = Task.WaitAny(_listenerTasks.ToArray(), _config.NetworkTimeout);
            if (removaAtIndex > 0)
                _listenerTasks.RemoveAt(removaAtIndex);
        }

        private void ProcessConnectionFromClient(TcpClient client)
        {
            Console.WriteLine("Connection established on Thread " + Thread.CurrentThread.ManagedThreadId);

            if (!client.Connected) //Abort if not connected
                return;
            
            Console.WriteLine($"From host:{client.Client.RemoteEndPoint}");
            var session = CreateSession(client);

            _sessions.TryAdd(session.Id, session);
        }

        private Session CreateSession(TcpClient client)
        {
            var session = new Session(this);
            session.Disconnecting += SessionOnDisconnecting;
            session.DataReceived += SessionOnDataReceived;
            session.Connect(client.Client);
            OnSessionCreated(new SessionCreatedEventArgs());
            return session;
        }

        private void SessionOnDataReceived(DataReceivedEventArgs ea)
        {
            Console.WriteLine($"Received data:{ea.Data}");
            OnDataReceived(ea);
        }

        private void SessionOnDisconnecting(Session session)
        {
            Console.WriteLine($"Session disconnecting:{session.Id}");
        }


        internal void UnregisterSession(Guid sessionId)
        {
            _sessions.TryRemove(sessionId, out Session temp);
            
        }

        public void Send(Guid sessionId, string data)
        {
            if (_sessions.ContainsKey(sessionId))
            {
                Session session = _sessions[sessionId];
                session.SendStringAsync(data);
            }
        }
        
    }
}