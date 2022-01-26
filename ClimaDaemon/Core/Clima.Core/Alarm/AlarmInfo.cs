namespace Clima.Core.Alarm
{
    public class AlarmInfo
    {
        public AlarmInfo(string key, string name = "", string message = "")
        {
            Key = key;
            Name = name;
            Message = message;
        }

        public string Key { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
    }
}