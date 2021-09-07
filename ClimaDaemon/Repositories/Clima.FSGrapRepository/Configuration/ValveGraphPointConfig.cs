using System;

namespace Clima.FSGrapRepository.Configuration
{
    public class ValveGraphPointConfig:IGraphPointConfig<ValveGraphPointConfig>,
        IComparable<ValveGraphPointConfig>
    {
        public ValveGraphPointConfig()
        {
            
        }

        public ValveGraphPointConfig(float vent = 0, float valve =0)
        {
            Ventilation = vent;
            Valve = valve;
        }
        public int CompareTo(IGraphPointConfig<ValveGraphPointConfig>? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;

            return -1;
        }
        public float Ventilation { get; set; }
        public float Valve { get; set; }
        public int Index { get; }
        
        public int CompareTo(ValveGraphPointConfig? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            
            var ventComparison = Ventilation.CompareTo(other.Ventilation);
            if (ventComparison != 0) return ventComparison;
            
            return Index.CompareTo(other.Index);
        }
    }
}