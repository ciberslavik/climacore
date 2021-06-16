using System;

namespace Clima.TcpServer.CoreServer
{
    public class DataReceivedEventArgs:EventArgs
    {
        private readonly string _data;
        private readonly int _sessionId;

        public DataReceivedEventArgs(int sessionId, string data = "")
        {
            _sessionId = sessionId;
            _data = data;
        }

        public string Data => _data;

        public int SessionId => _sessionId;
    }
}