namespace Clima.Core.Conrollers.Ventilation.Ventilation.Configuration
{
    public class FanConfig
    {
        public string FanName { get; set; }
        public string RelayName { get; set; }
        public string FrequencyConverterName { get; set; }
        public FanType FanType { get; set; }
        public int FanId { get; set; }
        public int Performance { get; set; }
        public int FansCount { get; set; }
        public int FanPriority { get; set; }
        public bool Hermetise { get; set; }
        public bool Disabled { get; set; }
        public double StartPower { get; set; }
        public double StopPower { get; set; }
    }

    public enum FanType : int
    {
        Analog = 0,
        Discrete = 1
    }
}