namespace Clima.Core.Network.Messages
{
    public class ClimatStateResponse
    {
        public ClimatStateResponse()
        {
            
        }
        
        public float FrontTemperature { get; set; }
        public float RearTemperature { get; set; }
        public float OutdoorTemperature { get; set; }
        public float Humidity { get; set; }
        public float Pressure { get; set; }
        public float TempSetPoint { get; set; }
        
        public float AnalogFanPower { get; set; }
        public float ValvePosition { get; set; }
        public float MinePosition { get; set; }
        public float TotalFanPerformance { get; set; }
        public float VentilationSetPoint { get; set; }
    }
}