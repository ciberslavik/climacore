using System.IO;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
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
                NetworkStream s = client.GetStream();
                    
                
                // Encode a test message into a byte array.
                // Signal the end of the message using the "<EOF>".
                byte[] messsage = Encoding.UTF8.GetBytes("Hello from the client.<EOF>");
                // Send hello message to the server.
                s.Write(messsage);
                s.Flush();
                // Read message from the server.
                string serverMessage = ReadMessage(s);
                Console.WriteLine("Server says: {0}", serverMessage);
                // Close the client connection.
                client.Close();
                Console.WriteLine("Client closed.");
            }
        }
        private string ReadMessage(Stream sslStream)
        {
            byte [] buffer = new byte[2048];
            StringBuilder messageData = new StringBuilder();
            int bytes = -1;
            do
            {
                bytes = sslStream.Read(buffer, 0, buffer.Length);

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
        
        protected virtual void OnServerResponce(Message message)
        {
            ServerResponce?.Invoke(message);
        }
    }
}