using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataContract;

namespace Clima.TcpServer
{
    public class StateObject
    {
        // Size of receive buffer.  
        public const int BufferSize = 1024;

        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];

        // Received data string.
        public StringBuilder sb = new StringBuilder();

        // Client socket.
        public Socket workSocket = null;
    }  
    
    public class Server
    {
        private ServerConfig _config;
        private readonly IDataSerializer _serializer;
        private List<ClientWorker> _workers;
        
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public delegate void MessageReceivedHandler(Message message);

        public event MessageReceivedHandler MessageReceived;

        public Server(ServerConfig config, IDataSerializer serializer)
        {
            _config = config;
            _serializer = serializer;
            _workers = new List<ClientWorker>();
        }

        public void StartServer()
        {
            //_listenerThread.Start();
            // Establish the local endpoint for the socket.  
            // The DNS name of the computer  
            // running the listener is "host.contoso.com".  
            IPAddress ipAddress = IPAddress.Parse(_config.Host);  
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, _config.Port);  
  
            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,  
                SocketType.Stream, ProtocolType.Tcp );  
  
            // Bind the socket to the local endpoint and listen for incoming connections.  
            try {  
                listener.Bind(localEndPoint);  
                listener.Listen(100);  
  
                while (true) {  
                    // Set the event to nonsignaled state.  
                    allDone.Reset();  
  
                    // Start an asynchronous socket to listen for connections.  
                    Console.WriteLine("Waiting for a connection...");  
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),  
                        listener );  
  
                    // Wait until a connection is made before continuing.  
                    allDone.WaitOne();  
                }  
  
            } catch (Exception e) {  
                Console.WriteLine(e.ToString());  
            }  
  
            Console.WriteLine("\nPress ENTER to continue...");  
            Console.Read();  
        }

        public void WaitStopServer()
        {
            //_exitSignal = true;
            Console.WriteLine("Exit Signal sent to server thread");
            Console.WriteLine("Join server thread");
            //_listenerThread.Join();
            Console.WriteLine("Server thread has stopped");
        }

        protected virtual void Run()
        {
            /*Console.WriteLine($"Server started thread id:{Thread.CurrentThread.ManagedThreadId}");

            if (_isRunning)
                return;
            _isRunning = true;
            _listener.Start();
            _exitSignal = false;

            while (!_exitSignal)
                ConnectionLooper();

            _isRunning = false;*/
        }

        protected virtual void ConnectionLooper()
        {
            /*while (_clientTasks.Count < _config.MaxConcurentListeners)
            {
                var AwaiterTask = Task.Run(async () =>
                {
                    Console.WriteLine($"Listening on thread:{Thread.CurrentThread.ManagedThreadId}");
                    ProcessConnectionFromClient(await _listener.AcceptTcpClientAsync());
                });
                _clientTasks.Add(AwaiterTask);
            }

            int removaAtIndex = Task.WaitAny(_clientTasks.ToArray(), _config.Timeout);
            if (removaAtIndex > 0)
                _clientTasks.RemoveAt(removaAtIndex);*/
        }

        /*protected virtual void ProcessConnectionFromClient(TcpClient Connection)
        {
            Console.WriteLine("Connection established on Thread " + Thread.CurrentThread.ManagedThreadId);

            if (!Connection.Connected) //Abort if not connected
                return;

            Console.WriteLine($"From host:{Connection.Client.RemoteEndPoint}");
            _workers.Add(new ClientWorker(Connection));

        }
        */
        protected virtual void OnMessageReceived(Message message)
        {
            MessageReceived?.Invoke(message);
        }
        
        private void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            allDone.Set();  
  
            // Get the socket that handles the client request.  
            Socket listener = (Socket) ar.AsyncState;  
            Socket handler = listener.EndAccept(ar);  
  
            var worker = new ClientWorker(handler,_serializer);
            worker.MessageReceived += ClientMessageReceived;
            worker.Process();
            _workers.Add(worker);
        }

        private NetworkReply ClientMessageReceived(NetworkRequest request)
        {
            NetworkReply reply = new NetworkReply();
            string data = Encoding.UTF8.GetString(request.Data);
            Console.WriteLine($"Request:{request.RequestType}\n Data:{data}");

            return reply;
        }

        private void Send(Socket handler, String data)
        {
              
        }

       
    }
}