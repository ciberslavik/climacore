namespace Clima.Core.Controllers.Heater
{
    public class HeaterState
    {
        public HeaterState()
        {
        }

        public bool IsRunning { get; set; } = false;
        public bool IsManual { get; set; } = false;
    }
}