using System;

namespace Clima.AgavaModBusIO.Transport
{
    public delegate void ReplyReceivedEventHandler(object sender, ReplyReceivedEventArgs ea);

    public class ReplyReceivedEventArgs : EventArgs
    {
        private AgavaReply _reply;

        public ReplyReceivedEventArgs()
        {
            _reply = new AgavaReply();
        }

        public ReplyReceivedEventArgs(AgavaReply reply)
        {
            _reply = reply;
        }

        public AgavaReply Reply
        {
            get => _reply;
            set => _reply = value;
        }
    }
}