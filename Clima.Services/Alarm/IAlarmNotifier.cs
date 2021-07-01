namespace Clima.Services.Alarm
{
    public delegate void AlarmNotifyHandler(AlarmNotifyEventArgs ea);

    public interface IAlarmNotifier
    {
        event AlarmNotifyHandler AlarmNotify;
    }
}