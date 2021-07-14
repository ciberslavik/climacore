
using Clima.Basics.Services;
using Clima.Basics.Services.Communication;
using Clima.Basics.Services.Communication.Messages;
using Clima.Communication;
using Clima.NetworkServer;
using Clima.NetworkServer.Services;
using Clima.NetworkServer.Transport;
using Clima.NetworkServer.Transport.TcpSocket;
using Clima.Serialization.Newtonsoft;
using ConsoleServer.Services;

namespace ConsoleServer
{
    public class ClimaServer
    {
        private IServer _tcpServer;
        private JsonServer _server;
        private INetworkSerializer _serializer;
        private IMessageTypeProvider _typeProvider;
        private IMessageNameProvider _nameProvider;
        private IServiceExecutor _executor;
        public ClimaServer()
        {
            _serializer = new NetworkSerializer();
            _tcpServer = new TcpSocketServer(TcpServerConfig.CreateDefault());
            _typeProvider = new MessageTypeProvider();
            _executor = new ServiceExecutor(default(IServiceProvider));
            _executor.RegisterHandler(VersionRequest.MessageName, param =>
            {
                return new VersionService().Execute((VersionRequest) param);
            });
            
            
            _server = new JsonServer(_tcpServer, _serializer, _typeProvider, _executor);
        }

        public void Start()
        {
            _tcpServer.Start();
        }

        public void Stop()
        {
            _tcpServer.Stop();
        }
    }
}