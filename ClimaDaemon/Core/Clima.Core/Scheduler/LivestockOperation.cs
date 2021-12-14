using System;

namespace Clima.Core.Scheduler
{
    public class LivestockOperation:IComparable<LivestockOperation>
    {
        public LivestockOperation()
        {
            
        }
        public int HeadCount { get; set; }
        public LivestockOpType OperationType { get; set; }
        public DateTime OperationDate { get; set; }

        public int CompareTo(LivestockOperation? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return OperationDate.CompareTo(other.OperationDate);
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