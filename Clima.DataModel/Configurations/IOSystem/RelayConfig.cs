using Clima.DataModel.Configurations.IOSystem;

namespace Clima.Services.Devices.Configs
{
    public class RelayConfig:DeviceConfigBase
    {
        public RelayConfig()
        {
        }
        public static RelayConfig CreateDefaultConfig()
        {
            RelayConfig relay = new RelayConfig();
            relay.EnablePinName = "DO:1:1";
            relay.MonitorPinName = "DI:1:1";
            relay.EnableLevel = ActiveLevel.High;
            relay.MonitorLevel = ActiveLevel.High;
            relay.MonitorTimeout = 200;
            relay.RelayName = "REL:1";

            return relay;
        }
        public string EnablePinName { get; set; }
        public string MonitorPinName { get; set; }
        public ActiveLevel EnableLevel { get; set; }
        public ActiveLevel MonitorLevel { get; set; }
        public int MonitorTimeout { get; set; }
        public string RelayName { get; set; }
        
    }

    public enum ActiveLevel
    {
        High,
        Low
    }
    
}