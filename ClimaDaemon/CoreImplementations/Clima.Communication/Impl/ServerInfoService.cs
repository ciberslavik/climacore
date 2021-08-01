using Clima.Communication.Messages;
using Clima.Communication.Services;

namespace Clima.Communication.Impl
{
    public class ServerInfoService:IServerInfoService
    {
        public ServerInfoService()
        {
        }

        public static string MessageName => "ServerInfo";

        public ServerInfoVersionResponse GetServerVersion(ServerInfoVersionRequest versionRequest)
        {
            return new ServerInfoVersionResponse()
            {
                Version = "ClimaServer 0.1a"
            };
        }
    }
}