using System;

namespace Clima.Core.Controllers.Light
{
    public class LightTimerItem:IComparable<LightTimerItem>
    {
        public LightTimerItem()
        {
            OnTime = DateTime.MinValue;
            OffTime = DateTime.MinValue;
        }
        public LightTimerItem(DateTime onTime, DateTime offTime)
        {
            OnTime = onTime;
            OffTime = offTime;
        }
        public DateTime OnTime { get; set; }
        public DateTime OffTime { get; set; }
        /// <summary>
        /// Check contains time in current interval, does not include year and month day
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool ContainsTime(DateTime time)
        {
            if ((time.Hour > OnTime.Hour) && (time.Minute > OnTime.Minute) && (time.Second > OnTime.Second) &&
                (time.Hour < OffTime.Hour) && (time.Minute < OffTime.Minute)&& (time.Second < OffTime.Second))
            {
                return true;
            }

            return false;
        }

        public int CompareTo(LightTimerItem? other)
        {
            if (other is null)
                return 1;
            else
                return (OnTime.Second - other.OnTime.Second);
        }
    }
}