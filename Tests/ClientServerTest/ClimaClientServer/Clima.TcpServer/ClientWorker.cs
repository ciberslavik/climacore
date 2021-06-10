using System.Net.Sockets;

namespace Clima.TcpServer
{
    public class ClientWorker
    {
        private Socket _socket;
        public ClientWorker(Socket socket)
        {
            _socket = socket;
        }

        
    }
}