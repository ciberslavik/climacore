using System;

namespace Clima.Core.Devices
{
    public delegate void AlarmNotifyEventHandler(object sender, AlarmNotifyEventArgs ea);

    public class AlarmNotifyEventArgs : EventArgs
    {
        private string _message;

        public AlarmNotifyEventArgs(string message = "")
        {
            _message = message;
        }

        public string Message
        {
            get => _message;
            set => _message = value;
        }
    }
}