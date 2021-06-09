using System.Net.Sockets;
using Clima.DataModel.Security;

namespace Clima.TcpServer.Client
{
    public class ClimaClient:Services.Communication.Client
    {
        private readonly  TcpClient _client;
        public ClimaClient(TcpClient client)
        {
            _client = client;
        }
        public User User { get; set; }

        public bool UserValid()
        {
            return true;
        }
    }
}