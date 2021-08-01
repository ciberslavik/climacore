using Clima.Communication.Messages;

namespace Clima.Communication.Services
{
    public class ServerInfoService
    {
        public ServerInfoService()
        {
        }

        public static string MessageName => "ServerInfo";

        public ServerInfoResponse GetServerVersion(ServerInfoRequest request)
        {
            return new ServerInfoResponse()
            {
                Version = "0.0.1"
            };
        }
    }
}