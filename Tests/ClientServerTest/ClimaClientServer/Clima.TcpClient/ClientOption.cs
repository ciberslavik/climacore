namespace Clima.TcpClient
{
    public class ClientOption
    {
        public ClientOption()
        {
            
        }
        public string Host { get; set; }
        public int Port { get; set; }
        public int ReceiveBufferSize { get; set; } = 2048;
        public int ReceiveBufferLimit { get; set; } = 0;
        public int SendBufferSize { get; set; } = 2048;
        public int SendBufferLimit { get; set; } = 0;

    }
}