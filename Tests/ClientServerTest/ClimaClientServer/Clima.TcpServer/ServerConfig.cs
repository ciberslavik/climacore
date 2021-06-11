namespace Clima.TcpServer
{
    public class ServerConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public int MaxConcurentListeners { get; set; }
        public int Timeout { get; set; }
        public string CertFile { get; set; }
    }
}