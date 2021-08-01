using Clima.Communication.Messages;

namespace Clima.Communication.Services
{
    public interface IServerInfoService
    {
        ServerInfoVersionResponse GetServerVersion(ServerInfoVersionRequest versionRequest);
    }
}