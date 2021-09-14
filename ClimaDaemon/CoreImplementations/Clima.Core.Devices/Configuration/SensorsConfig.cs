namespace Clima.Core.Devices.Configuration
{
    public class SensorsConfig
    {
        public SensorsConfig()
        {
        }

        public string FrontTempPinName { get; set; }
        public string RearTempPinName { get; set; }
        public string OutdoorTempPinName { get; set; }
        public string HumidityPinName { get; set; }
        public string PressurePinName { get; set; }
	public string Valve1PinName {get; set;}
	public string Valve2PinName {get; set;}
        public static SensorsConfig CreateDefault()
        {
            var c = new SensorsConfig();
            c.FrontTempPinName = "AI:1:0";
            c.RearTempPinName = "AI:1:1";
            c.HumidityPinName = "AI:1:2";
            c.PressurePinName = "AI:1:3";
            c.OutdoorTempPinName = "AI:1:6";
	    c.Valve1PinName = "AI:1:4";
	    c.Valve2PinName = "AI:1:5";
            return c;
        }
    }
}