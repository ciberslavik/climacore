namespace Clima.Core.DataModel
{
    public class HeaterState
    {
        public HeaterState()
        {
        }
        
        public HeaterInfo Info { get; set; }
        public float SetPoint { get; set; }
        
        public bool IsRunning { get; set; } = false;

    }
}