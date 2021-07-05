using Clima.NetworkServer;
using Clima.NetworkServer.Messages;

namespace ConsoleServer
{
    public class VersionResponse
    {
        public string ProductName { get; set; }
        public string ProductVersion { get; set; }
        public string EngineVersion { get; set; }
    }
}