using System.Net.Sockets;

namespace Clima.TcpServer.CoreServer
{
    public class Session
    {
        private readonly Server _server;
        private readonly Socket _socket;

        //Session statistics
        private long _bytesSend;
        private long _bytesReceive;

        public int SendBufferSize { get; set; } = 1024;
        public int ReceiveBufferSize { get; set; } = 1024;
        public Session(Server server)
        {
            _server = server;
        }

        internal void Connect(Socket socket)
        {
            
        }
        
    }
}