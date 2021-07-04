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
    }
}