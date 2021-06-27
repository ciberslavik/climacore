namespace Clima.TcpServer.CoreServer
{
    public class ServerConfig
    {
        public ServerConfig()
        {
            
        }
        public int ReceiveBufferLimit { get; set; } = 0;
        public int ReceiveBufferSize { get; set; } = 2048;
        public int SendBufferLimit { get; set; } = 0;
        public int SendBufferSize { get; set; } = 2048;

        public int NetworkTimeout { get; set; } = 300;
    }
}