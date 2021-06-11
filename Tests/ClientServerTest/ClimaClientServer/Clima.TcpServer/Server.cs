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
    public class Server
    {
        private ServerConfig _config;
        private readonly IDataSerializer _serializer;
        private TcpListener _listener;
        private Thread _listenerThread;
        private List<Task> _clientTasks = new List<Task>();
        private bool _exitSignal;
        private bool _isRunning;
        private X509Certificate _certificate;

        public delegate void MessageReceivedHandler(Message message);

        public event MessageReceivedHandler MessageReceived;

        public Server(ServerConfig config, IDataSerializer serializer)
        {
            _config = config;
            _serializer = serializer;
            _certificate = X509Certificate.CreateFromCertFile(_config.CertFile);

            _listenerThread = new Thread(Run);
            _listener = new TcpListener(IPAddress.Any, _config.Port);
        }

        public void StartServer()
        {
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

            if (_isRunning)
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
                    ProcessConnectionFromClient(await _listener.AcceptTcpClientAsync());
                });
                _clientTasks.Add(AwaiterTask);
            }

            int removaAtIndex = Task.WaitAny(_clientTasks.ToArray(), _config.Timeout);
            if (removaAtIndex > 0)
                _clientTasks.RemoveAt(removaAtIndex);
        }

        protected virtual void ProcessConnectionFromClient(TcpClient Connection)
        {
            Console.WriteLine("Connection established on Thread " + Thread.CurrentThread.ManagedThreadId);

            if (!Connection.Connected) //Abort if not connected
                return;

            Console.WriteLine($"From host:{Connection.Client.RemoteEndPoint}");

            NetworkStream s = Connection.GetStream();
            
            try
            {
                string data = ReadMessage(s);
                Console.WriteLine(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            //Console.WriteLine("Client disconnected from Thread " + Thread.CurrentThread.ManagedThreadId);
        }

        private string ReadMessage(Stream clientStream)
        {
            byte [] buffer = new byte[2048];
            StringBuilder messageData = new StringBuilder();
            int bytes = -1;
            do
            {
                bytes = clientStream.Read(buffer, 0, buffer.Length);

                // Use Decoder class to convert from bytes to UTF8
                // in case a character spans two buffers.
                Decoder decoder = Encoding.UTF8.GetDecoder();
                char[] chars = new char[decoder.GetCharCount(buffer,0,bytes)];
                decoder.GetChars(buffer, 0, bytes, chars,0);
                messageData.Append (chars);
                // Check for EOF or an empty message.
                if (messageData.ToString().IndexOf("<EOF>") != -1)
                {
                    break;
                }
            } while (bytes !=0);
            
            return messageData.ToString();
        }
        protected virtual void OnMessageReceived(Message message)
        {
            MessageReceived?.Invoke(message);
        }
    }
}