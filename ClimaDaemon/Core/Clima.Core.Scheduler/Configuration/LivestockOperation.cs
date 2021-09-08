using System;

namespace Clima.Core.Scheduler.Configuration
{
    public class LivestockOperation:IComparable<LivestockOperation>
    {
        public LivestockOperation()
        {
            
        }
        public int HedCount { get; set; }
        public LivestockOpType OpertionType { get; set; }
        public DateTime OpertionDate { get; set; }

        public int CompareTo(LivestockOperation other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return OpertionDate.CompareTo(other.OpertionDate);
        }
    }

    public enum LivestockOpType
    {
        Planted,
        Killed,
        Death,
        Refracted
    }
}