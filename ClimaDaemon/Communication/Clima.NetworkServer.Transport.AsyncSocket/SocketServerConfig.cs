using Clima.Basics.Configuration;

namespace SocketAsyncServer
{
    public class SocketServerConfig:IConfigurationItem
    {
        public SocketServerConfig()
        {
            
        }
        public string ConfigurationName => "SocketServerConfig";
        
        public int MaxConnections { get; set; }
        public int Port { get; set; }
        public int BufferSize { get; set; }

        public static SocketServerConfig CreateDefault()
        {
            return new SocketServerConfig()
            {
                BufferSize = 10240,
                MaxConnections = 10,
                Port = 5911
            };
        }
    }
}