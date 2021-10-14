namespace Clima.Core.DataModel
{
    public class HeaterState
    {
        public HeaterState()
        {
        }
        public float SetPoint { get; set; }
        public float CorrectedSetPoint { get; set; }
        public bool IsRunning { get; set; } = false;

    }
}