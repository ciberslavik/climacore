namespace Clima.Core.DataModel
{
    public class VentilationParams
    {
        public VentilationParams()
        {
            
        }
        public float Proportional { get; set; } = 0;
        public float Integral { get; set; } = 0;
        public float Differential { get; set; } = 0;

        public float CorrectionMax { get; set; } = 0;
        public float CorrectionMin { get; set; } = 0;
    }
}