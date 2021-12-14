namespace Clima.FSGrapRepository.Configuration
{
    public class VentCorrectionGraphPointConfig:IGraphPointConfig<VentCorrectionGraphPointConfig>
    {
        private int _index;

        public VentCorrectionGraphPointConfig()
        {
            OutdoorTemp = 0;
            CorrectionValue = 0;
        }

        public VentCorrectionGraphPointConfig(float outdoorTemp, float correctionValue)
        {
            OutdoorTemp = outdoorTemp;
            CorrectionValue = correctionValue;
        }
        public int CompareTo(IGraphPointConfig<VentCorrectionGraphPointConfig>? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;

            return -1;
        }
        public float OutdoorTemp { get; set; }
        public float CorrectionValue { get; set; }
        
        public int Index => _index;
    }
}