using System;

namespace Clima.FSGrapRepository.Configuration
{
    public class VentilationGraphPointConfig:IGraphPointConfig<VentilationGraphPointConfig>,
        IComparable<VentilationGraphPointConfig>
    {
        public VentilationGraphPointConfig()
        {
            Day = 0;
            MinValue = 0;
            MaxValue = 0;
        }
        public VentilationGraphPointConfig(int day = 0, float min = 0, float max = 0)
        {
            Day = day;
            MinValue = min;
            MaxValue = max;
        }

        public int Day { get; set; }
        public float MaxValue { get; set; }
        public float MinValue { get; set; }
        
        public int Index { get; }
        
        public int CompareTo(IGraphPointConfig<VentilationGraphPointConfig>? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;

            return -1;
        }
        public int CompareTo(VentilationGraphPointConfig? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            
            var dayComparison = Day.CompareTo(other.Day);
            if (dayComparison != 0) return dayComparison;
            
            return Index.CompareTo(other.Index);
        }
    }
}