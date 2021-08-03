using System;

namespace Clima.FSGrapRepository.Configuration
{
    public class TemperatureGraphPointConfig:IGraphPointConfig<TemperatureGraphPointConfig>, IComparable<TemperatureGraphPointConfig>
    {
        public TemperatureGraphPointConfig()
        {
        }
        public int Day { get; set; }
        public float Temperature { get; set; }

        public int Index { get; }

        public int CompareTo(TemperatureGraphPointConfig other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var dayComparison = Day.CompareTo(other.Day);
            if (dayComparison != 0) return dayComparison;
            var temperatureComparison = Temperature.CompareTo(other.Temperature);
            if (temperatureComparison != 0) return temperatureComparison;
            return Index.CompareTo(other.Index);
        }

        public int CompareTo(IGraphPointConfig<TemperatureGraphPointConfig>? other)
        {
            throw new NotImplementedException();
        }
    }
}