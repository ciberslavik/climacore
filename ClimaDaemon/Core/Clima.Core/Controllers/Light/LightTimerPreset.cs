using System.Collections.Generic;

namespace Clima.Core.Controllers.Light
{
    public class LightTimerPreset
    {
        public LightTimerPreset()
        {
        }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<LightTimerItem> Timers { get; }=new List<LightTimerItem>();
    }
}