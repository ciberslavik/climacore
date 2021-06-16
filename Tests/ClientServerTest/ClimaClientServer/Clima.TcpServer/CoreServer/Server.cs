using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Clima.TcpServer.CoreServer
{
    public class Server
    {
        #region Private Fields
        private const int MAX_LISTENER = 3;
        private TcpListener _listener;
        private Thread _listenerThread;
        private bool _exitSignal;
        private List<Task> _listenerTasks;
        private ConcurrentDictionary<Guid, Session> _sessions;
        #endregion Private Fields
        public bool IsRunning { get; private set; }
        
        public Server(string host = "127.0.0.1", int port = 5911)
        {
            _listenerTasks = new List<Task>();
            _sessions = new ConcurrentDictionary<Guid, Session>();
            
            _listener = new TcpListener(IPAddress.Parse(host), port);
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
            int removaAtIndex = Task.WaitAny(_listenerTasks.ToArray(), 500);
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
            
            return session;
        }

        private void SessionOnDataReceived(DataReceivedEventArgs ea)
        {
            Console.WriteLine($"Received data:{ea.Data}");
        }

        private void SessionOnDisconnecting()
        {
            throw new NotImplementedException();
        }
        
        
    }
}