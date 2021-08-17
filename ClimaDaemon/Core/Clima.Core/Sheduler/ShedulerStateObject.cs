namespace Clima.Core.Sheduler
{
    public class ShedulerStateObject
    {
        public ShedulerStateObject()
        {
        }

        public ShedulerState State { get; set; }
        
        public float TemperatureSetPoint { get; set; }
        public float VentilationMax { get; set; }
        public float VentilationMin { get; set; }
        
        public string TemperatureGraphKey { get; set; }
        public string VentMinMaxGraphKey { get; set; }
        
    }
}