namespace Clima.Core.Alarm
{
    public delegate void AlarmNotifyHandler(AlarmNotifyEventArgs ea);

    public interface IAlarmNotifier
    {
        event AlarmNotifyHandler AlarmNotify;
    }
}