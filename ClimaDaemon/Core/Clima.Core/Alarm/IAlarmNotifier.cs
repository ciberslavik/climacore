namespace Clima.Core.Alarm
{
    public interface IAlarmNotifier
    {
        event AlarmNotifyHandler Notify;
        AlarmConfigBase Configuration { get; set; }
    }
}