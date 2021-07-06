using Clima.Basics.Services.Communication;
using Clima.Basics.Services.Communication.Messages;

namespace ConsoleServer
{
    public class VersionRequest:IReturn<VersionResponse>,ICustomName
    {
        public const string MessageName = "rpc.version";

        string ICustomName.MessageName => MessageName;
    }
}