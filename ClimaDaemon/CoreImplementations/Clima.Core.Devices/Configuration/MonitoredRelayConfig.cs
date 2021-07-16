namespace Clima.Core.Devices.Configuration
{
    public class MonitoredRelayConfig
    {
        public MonitoredRelayConfig()
        {
        }

        public static MonitoredRelayConfig CreateDefault(int relayNumber)
        {
            var config = new MonitoredRelayConfig();
            config.RelayName = $"Relay{relayNumber}";
            config.ControlPinName = $"DO:1:{relayNumber + 2}";
            config.MonitorPinName = $"DI:1:{relayNumber + 2}";
            config.StateChangeTimeout = 600;
            config.MonitorTimeout = 300;
            return config;
        }
        public string RelayName { get; set; }
        public string ControlPinName { get; set; }
        public string MonitorPinName { get; set; }
        public int StateChangeTimeout { get; set; }
        public int MonitorTimeout { get; set; }
        public ActiveLevel EnableLevel { get; set; }
        public ActiveLevel MonitorLevel { get; set; }
    }
    public enum ActiveLevel
    {
        High,
        Low
    }
}