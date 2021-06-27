using System;

namespace Clima.TcpClient
{
    public class DataReceivedEventArgs:EventArgs
    {
        public DataReceivedEventArgs(string data="")
        {
            Data = data;
        }
        public string Data { get; set; }
    }
}