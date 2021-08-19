namespace Clima.Core.Alarm
{
    public interface IAlarmManager
    {
        void RegisterNotifier(IAlarmNotifier notifier);
    }
}