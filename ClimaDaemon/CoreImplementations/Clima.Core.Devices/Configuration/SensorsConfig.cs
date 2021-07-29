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

        public static SensorsConfig CreateDefault()
        {
            var c = new SensorsConfig();
            c.FrontTempPinName = "AI:1:0";
            c.RearTempPinName = "AI:1:1";
            c.HumidityPinName = "AI:1:2";
            c.PressurePinName = "AI:1:3";
            c.OutdoorTempPinName = "AI:1:6";
            return c;
        }
    }
}