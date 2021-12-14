namespace Clima.Core.DataModel
{
    public class HeaterParams
    {
        public HeaterParams()
        {
        }

        public string Key { get; set; } = "";
        public string PinName { get; set; } = "";
        public float DeltaOn { get; set; } = 0.0f;
        public float DeltaOff { get; set; } = 0.0f;
        public float ManualSetPoint { get; set; } = 0.0f;
        public bool IsManual { get; set; }
        public int ControlZone { get; set; }
        public float Correction { get; set; } = 0.0f;
    }

    public enum HeaterControlZone : int
    {
        FrontZone = 0,
        RearZone = 1
    }
}