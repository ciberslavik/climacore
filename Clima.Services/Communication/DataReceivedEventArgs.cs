using System;

namespace Clima.Services.Communication
{
    public delegate void DataReceivedEventHandler(DataReceivedEventArgs ea);
    public class DataReceivedEventArgs:EventArgs
    {
        private readonly string _data;
        private readonly Guid _session;

        public DataReceivedEventArgs(Guid session, string data = "")
        {
            _session = session;
            _data = data;
        }

        public string Data => _data;

        public Guid Session => _session;
    }
}