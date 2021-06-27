using Clima.Services.Configuration;

namespace Clima.TcpServer.CoreServer
{
    public class ServerConfig:ConfigItemBase
    {
        public ServerConfig()
        {
            
        }

        public string Host { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 5911;
        public int ReceiveBufferLimit { get; set; } = 0;
        public int ReceiveBufferSize { get; set; } = 2048;
        public int SendBufferLimit { get; set; } = 0;
        public int SendBufferSize { get; set; } = 2048;

        public int NetworkTimeout { get; set; } = 300;
    }
}