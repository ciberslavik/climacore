namespace Clima.Core.Controllers.Light
{
    public class LightTimerProfileInfo
    {
        public LightTimerProfileInfo(string key = "", string name = "", string description = "")
        {
            Key = key;
            Name = name;
            Description = description;
        }

        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}