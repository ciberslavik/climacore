using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataContract;

namespace Clima.TcpServer
{
    public class Server
    {
        private ServerConfig _config;
        private readonly IDataSerializer _serializer;
        private TcpListener _listener;
        private Thread _listenerThread;
        private List<Task> _clientTasks = new List<Task>();
        private bool _exitSignal;
        private bool _isRunning;

        public delegate void MessageReceivedHandler(Message message);

        public event MessageReceivedHandler MessageReceived;
        public Server(ServerConfig config, IDataSerializer serializer)
        {
            _config = config;
            _serializer = serializer;
            _listenerThread = new Thread(Run);
            _listener = new TcpListener(IPAddress.Parse(_config.Host), _config.Port);
        }

        public void StartServer()
        {
            _exitSignal = false;
            _listenerThread.Start();
        }

        public void WaitStopServer()
        {
            _exitSignal = true;
            Console.WriteLine("Exit Signal sent to server thread");
            Console.WriteLine("Join server thread");
            _listenerThread.Join();
            Console.WriteLine("Server thread has stopped");
        }
        protected virtual void Run()
        {
            Console.WriteLine($"Server started thread id:{Thread.CurrentThread.ManagedThreadId}");
            
            if(_isRunning)
                return;
            _isRunning = true;
            _listener.Start();
            _exitSignal = false;

            while (!_exitSignal)
                ConnectionLooper();

            _isRunning = false;
        }

        protected virtual void ConnectionLooper()
        {
            while (_clientTasks.Count < _config.MaxConcurentListeners)
            {
                var AwaiterTask = Task.Run(async () =>
                {
                    Console.WriteLine($"Listening on thread:{Thread.CurrentThread.ManagedThreadId}");
                    ProcessMessagesFromClient(await _listener.AcceptTcpClientAsync());
                });
                _clientTasks.Add(AwaiterTask);
            }

            int removaAtIndex = Task.WaitAny(_clientTasks.ToArray(), _config.Timeout);
            if(removaAtIndex > 0)
                _clientTasks.RemoveAt(removaAtIndex);
        }

        protected virtual void ProcessMessagesFromClient(TcpClient Connection)
        {
            using (Connection)
            {
                Console.WriteLine("Connection established on Thread " + Thread.CurrentThread.ManagedThreadId);
                
                if (!Connection.Connected) //Abort if not connected
                    return;
                
                Console.WriteLine($"From host:{Connection.Client.RemoteEndPoint}");
                using (NetworkStream netstream = Connection.GetStream())
                {
                    if(!netstream.CanRead && !netstream.CanWrite)
                        return;
                    var writer = new StreamWriter(netstream) { AutoFlush = true };
                    var reader = new StreamReader(netstream);
                    
                    Message msg = new Message();
                    msg.Name = "Server message";
                    msg.Data = "Test server message";
                    string serverData = _serializer.Serialize(msg);
                    string response = "";
                    try
                    {
                        writer.Write(serverData + " \r\n");
                        response = reader.ReadToEnd();
                        Console.WriteLine(response);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }
            Console.WriteLine("Client disconnected from Thread " + Thread.CurrentThread.ManagedThreadId);
        }

        protected virtual void OnMessageReceived(Message message)
        {
            MessageReceived?.Invoke(message);
        }
    }
}