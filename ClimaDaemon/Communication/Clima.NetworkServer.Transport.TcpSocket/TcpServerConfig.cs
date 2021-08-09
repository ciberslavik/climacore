namespace Clima.NetworkServer.Transport.TcpSocket
{
    public class TcpServerConfig
    {
        public string HsotName { get; set; }
        public int Port { get; set; }
        public int MaxClientConnections { get; set; }

        public int SendBufferSize { get; set; }
        public int ReceiveBufferSize { get; set; }
        public int NetworkTimeout { get; set; }

        public static TcpServerConfig CreateDefault(string host, int port)
        {
            var config = new TcpServerConfig();

            config.HsotName = host;
            config.Port = port;
            config.MaxClientConnections = 3;
            config.NetworkTimeout = 300;
            config.ReceiveBufferSize = 1024;
            config.SendBufferSize = 1024;
            return config;
        }
    }
}