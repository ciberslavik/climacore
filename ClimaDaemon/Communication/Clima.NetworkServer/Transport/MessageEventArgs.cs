using System;

namespace Clima.NetworkServer.Transport
{
    public class MessageEventArgs : EventArgs
    {
        public string ConnectionId { get; set; }
        public string Data { get; set; }
        public string Result { get; set; }
    }
}