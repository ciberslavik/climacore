using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Clima.Services.Communication;
using Clima.TcpServer.Client;

namespace Clima.TcpServer
{
    public class TcpAppServer:IAppServer
    {
        private Thread _listenThread;
        private TcpListener _listener;

        private Dictionary<string, ClimaClient> _clients;
        public TcpAppServer()
        {
        }


        public void Start()
        {
            _listenThread = new Thread((e) =>
            {
                IPAddress addr = IPAddress.Parse("127.0.0.1");
                ListenWorker(addr,5911);
            });
            _listenThread.Start();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        private void ListenWorker(IPAddress addr, int port)
        {
            _listener = new TcpListener(addr, port);
            _listener.Start();
            while (true)
            {
                TcpClient tcpClient = _listener.AcceptTcpClient();
                ClimaClient client = new ClimaClient(tcpClient);
                
            }
        }
    }
}