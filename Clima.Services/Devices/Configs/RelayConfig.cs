namespace Clima.Services.Devices.Configs
{
    public class RelayConfig
    {
        public RelayConfig()
        {
        }

        public string RelayPinName { get; set; }
        public string MonitorPinName { get; set; }
        public ActiveEdge RelayEdge { get; set; }
        public ActiveEdge MonitorEdge { get; set; }
        public int MonitorTimeout { get; set; }
        public string RelayName { get; set; }
    }

    public enum ActiveEdge
    {
        Rising,
        Falling
    }
}