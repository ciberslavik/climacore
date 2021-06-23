namespace Clima.Core.Alarm
{
    public delegate void AlarmHandler(AlarmEventArgs ea);
    public interface IAlarmManager
    {
        event AlarmHandler Alarm;
        void AddNotifier(IAlarmNotifier notifier);
        void RemoveNotifier(IAlarmNotifier notifier);
    }
}