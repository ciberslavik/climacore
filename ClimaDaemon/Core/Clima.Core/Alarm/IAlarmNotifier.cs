using System;

namespace Clima.Core.Alarm
{
    public interface IAlarmNotifier
    {
        event EventHandler<AlarmEventArgs> Notify;
        bool IsAlarm { get; }
        bool Reset();
    }
}