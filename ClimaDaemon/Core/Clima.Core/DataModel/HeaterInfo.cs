namespace Clima.Core.DataModel
{
    public class HeaterInfo
    {
        public HeaterInfo()
        {
        }

        public string Key { get; set; }
        public string PinName { get; set; }
        public float Hysteresis { get; set; }
        public float ManualSetPoint { get; set; }
        public bool IsManual { get; set; }
        public int ControlZone { get; set; }
    }

    public enum HeaterControlZone : int
    {
        FrontZone = 0,
        RearZone = 1
    }
}