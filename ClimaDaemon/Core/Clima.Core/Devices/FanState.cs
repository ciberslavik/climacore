namespace Clima.Core.Devices
{
    public class FanState
    {
        public FanState()
        {
            
        }
        public int FanId { get; set; }
        public string FanName { get; set; }
        public bool Disabled { get; set; }
        public bool Hermetise { get; set; }
        public int Performance { get; set; }
        public int FansCount { get; set; }
        public int Priority { get; set; }
        
        public double StartValue { get; set; }
        public double StopValue { get; set; }
    }
}