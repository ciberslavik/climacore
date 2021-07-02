namespace Clima.Core.Ventelation
{
    public class AnalogFanConfig
    {
        public AnalogFanConfig()
        {
        }
        public string FanName { get; set; }
        public string FCName { get; set; }
        public double Performance { get; set; }
        public double MinimumPower { get; set; }
    }
}