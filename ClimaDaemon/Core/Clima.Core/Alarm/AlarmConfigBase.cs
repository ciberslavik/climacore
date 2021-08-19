namespace Clima.Core.Alarm
{
    public abstract class AlarmConfigBase
    {
        public AlarmConfigBase()
        {
        }

        public string Title { get; set; }
        public string Description { get; set; }
    }
}