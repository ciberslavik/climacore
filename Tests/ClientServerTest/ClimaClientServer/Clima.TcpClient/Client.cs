using System.IO;
using System.Text;
using DataContract;

namespace Clima.TcpClient
{
    using System;
    using System.Net.Sockets;
    using System.Threading;

    public class Client
    {
        private bool _exitSignal;
        private string _host;
        private int _port;
        private readonly IDataSerializer _serializer;

        public delegate void ServerResponseHandler(Message message);

        public event ServerResponseHandler ServerResponce;
        
        public Client(string host, int port, IDataSerializer serializer)
        {
            _host = host;
            _port = port;
            _serializer = serializer;
        }

        public void SendMessage(Message message)
        {
            using (var client = new TcpClient())
            {
                try
                {
                    client.Connect(_host, _port);
                }
                catch (SocketException e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
                if(!client.Connected)
                    return;
                client.Client.SendBufferSize = 2048;
                
                using (NetworkStream netstream = client.GetStream())
                {
                    string data = _serializer.Serialize(message);
                    var writer = new StreamWriter(netstream) {AutoFlush = true};
                    var reader = new StreamReader(netstream);
                    string responce = "";
                    string clientData = _serializer.Serialize(message);
                    try
                    { 
                        writer.Write(clientData+" \r\n");
                        responce = reader.ReadToEnd();
                        Console.WriteLine(responce);
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }
        }

        protected virtual void OnServerResponce(Message message)
        {
            ServerResponce?.Invoke(message);
        }
    }
}