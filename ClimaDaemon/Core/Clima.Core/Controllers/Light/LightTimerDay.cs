using System;
using System.Collections.Generic;

namespace Clima.Core.Controllers.Light
{
    public class LightTimerDay:IComparable<LightTimerDay>
    {
        public LightTimerDay()
        {
        }

        public int Day { get; set; }
        public List<LightTimerItem> Timers { get; set; } = new List<LightTimerItem>();

        public int CompareTo(LightTimerDay? other)
        {
            if (other is null)
                return 1;
            
            return Day - other.Day;
        }
    }
}