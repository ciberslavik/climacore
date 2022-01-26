using System;

namespace Clima.Core.Alarm
{
    public class AlarmEventArgs:EventArgs
    {
        private AlarmInfo _alarmInfo;

        public AlarmEventArgs(string message)
        {
            _alarmInfo = new AlarmInfo(Guid.NewGuid().ToString());
            _alarmInfo.Message = message;
        }

        public AlarmEventArgs(AlarmInfo alarmInfo)
        {
            _alarmInfo = alarmInfo;
        }
        public string Message
        {
            get => _alarmInfo.Message;
            set => _alarmInfo.Message = value;
        }

        public AlarmInfo AlarmInfo
        {
            get => _alarmInfo;
            set => _alarmInfo = value;
        }
    }
}