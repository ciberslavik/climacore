using System.Collections.Generic;

namespace Clima.Core.Controllers.Light
{
    public class LightTimerProfile
    {
        public LightTimerProfile()
        {
            Key = "";
            Name = "";
            Description = "";
        }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<LightTimerDay> Days { get; }=new List<LightTimerDay>();
    }
}