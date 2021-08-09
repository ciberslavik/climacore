using Clima.Basics.Services.Communication;
using Clima.Communication.Messages;
using Clima.Communication.Services;

namespace Clima.Communication.Impl
{
    public class ServerInfoService : IServerInfoService, INetworkService
    {
        public ServerInfoService()
        {
        }

        public static string MessageName => "ServerInfo";

        [ServiceMethod]
        public ServerInfoVersionResponse GetServerVersion(ServerInfoVersionRequest versionRequest)
        {
            return new ServerInfoVersionResponse()
            {
                Version = "ClimaServer 0.1a"
            };
        }
    }
}