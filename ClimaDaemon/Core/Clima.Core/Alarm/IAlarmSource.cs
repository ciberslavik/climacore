using System;
using System.Collections.Generic;

namespace Clima.Core.Alarm
{
    public interface IAlarmSource
    {
        event EventHandler<AlarmEventArgs> Alarm;
        bool IsAlarm { get; }
        IEnumerable<AlarmInfo> ProvideAlarms { get; }
        void Reset();
    }
}