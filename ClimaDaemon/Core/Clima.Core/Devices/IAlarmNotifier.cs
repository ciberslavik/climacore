namespace Clima.Core.Devices
{
    public interface IAlarmNotifier
    {
        event AlarmNotifyEventHandler AlarmNotify;
        string NotifierName { get; }
    }
}