using System.Threading.Tasks;
using Grpc.Core;

namespace Clima.GrpcServer
{
    internal class AppServerService : AppServer.AppServerBase
    {
        public override Task<ServerInfoReply> ServerInfo(ServerInfoRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ServerInfoReply
            {
                ServerName = "ClimaProject gRPC server",
                VersionMinor = 1,
                VersionMajor = 0
            });   
        }
    }
}