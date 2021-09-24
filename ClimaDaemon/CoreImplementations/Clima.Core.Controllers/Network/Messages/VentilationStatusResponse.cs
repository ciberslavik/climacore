namespace Clima.Core.Controllers.Network.Messages
{
    public class VentilationStatusResponse
    {
        public float MineCurrentPos { get; set; }
        public float MineSetPoint { get; set; }
        public float ValveCurrentPos { get; set; }
        public float ValveSetPoint { get; set; }
        public float VentSetPoint { get; set; }
        
    }
}