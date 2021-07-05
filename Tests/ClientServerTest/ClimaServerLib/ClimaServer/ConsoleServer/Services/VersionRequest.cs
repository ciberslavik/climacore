using Clima.NetworkServer;
using Clima.NetworkServer.Messages;

namespace ConsoleServer
{
    public class VersionRequest:IReturn<VersionResponse>,ICustomName
    {
        public const string MessageName = "rpc.version";

        string ICustomName.MessageName => MessageName;
    }
}