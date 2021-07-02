using System.Runtime.CompilerServices;

namespace Clima.Services.Alarm
{
    public class AlarmNotifyEventArgs
    {
        private string _message;
        private string _senderName;
        public AlarmNotifyEventArgs(string message,[CallerMemberName]string senderName="")
        {
            _message = message;
            _senderName = senderName;
        }


        public string Message
        {
            get => _message;
            set => _message = value;
        }

        public string SenderName
        {
            get => _senderName;
            set => _senderName = value;
        }
    }
}