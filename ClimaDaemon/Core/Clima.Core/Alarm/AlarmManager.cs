namespace Clima.Core.Alarm
{
    public class AlarmManager:IAlarmManager
    {
        public void RegisterNotifier(IAlarmNotifier notifier)
        {
            
        }

        public void RegisterSource(IAlarmSource source)
        {
            source.Alarm += SourceOnAlarm;
        }

        private void SourceOnAlarm(object? sender, AlarmEventArgs e)
        {
            
        }
    }
}