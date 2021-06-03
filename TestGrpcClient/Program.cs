using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;

namespace TestGrpcClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // This switch must be set before creating the GrpcChannel/HttpClient.
            AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = 
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var channel = GrpcChannel.ForAddress("https://localhost:5001",
            new GrpcChannelOptions{HttpClient = new HttpClient(httpHandler)});

            var client = new AppServer.AppServerClient(channel);

            var reply = client.ServerInfo(new ServerInfoRequest());

            Console.WriteLine("ServerInfo reply:" + reply.ServerName + " version: " + reply.VersionMinor + "." + reply.VersionMajor);
            Console.ReadKey();
        }
    }
}
