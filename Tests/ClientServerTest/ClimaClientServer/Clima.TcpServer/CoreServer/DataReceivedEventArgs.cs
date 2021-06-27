using System;

namespace Clima.TcpServer.CoreServer
{
    public class DataReceivedEventArgs:EventArgs
    {
        private readonly string _data;
        private readonly Session _session;

        public DataReceivedEventArgs(Session session, string data = "")
        {
            _session = session;
            _data = data;
        }

        public string Data => _data;

        public Session Session => _session;
    }
}