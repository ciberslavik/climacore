using Clima.Basics.Configuration;

namespace Clima.NetworkServer.Transport.TcpSocket
{
    public class TcpServerConfig:IConfigurationItem
    {
        public string HsotName { get; set; }
        public int Port { get; set; }
        public int MaxClientConnections { get; set; }

        public int SendBufferSize { get; set; }
        public int ReceiveBufferSize { get; set; }
        public int NetworkTimeout { get; set; }

        public static TcpServerConfig CreateDefault()
        {
            var config = new TcpServerConfig();

            config.HsotName = "";
            config.Port = 5911;
            config.MaxClientConnections = 3;
            config.NetworkTimeout = 300;
            config.ReceiveBufferSize = 1024;
            config.SendBufferSize = 1024;
            return config;
        }

        public string ConfigurationName => FileName;
        public const string FileName = nameof(TcpServerConfig);
    }
}