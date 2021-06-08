namespace Clima.Services.Devices.Configs
{
    public class DiscreteFanConfigItem
    {
        public DiscreteFanConfigItem()
        {
        }

        public int Preformance { get; set; }
        public int FanCount { get; set; }
        public int FanPriority { get; set; }
        public string RelayPinName { get; set; }
        public string MonitorPinName { get; set; } 
        public bool Enabled { get; set; }
        
    }
}