namespace Clima.Core.Sheduler
{
    public class ShedulerStateObject
    {
        public ShedulerStateObject()
        {
        }

        public ShedulerState State { get; set; }
        
        public float TemperatureSetPoint { get; set; }
    }
}