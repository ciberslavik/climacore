namespace Clima.Core.DataModel
{
    public class HeaterParams
    {
        public HeaterParams()
        {
        }

        public string Key { get; set; }
        public string PinName { get; set; }
        public float DeltaOn { get; set; }
        public float DeltaOff { get; set; }
        public float ManualSetPoint { get; set; }
        public bool IsManual { get; set; }
        public int ControlZone { get; set; }
        public float Correction { get; set; }
    }

    public enum HeaterControlZone : int
    {
        FrontZone = 0,
        RearZone = 1
    }
}