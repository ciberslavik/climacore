namespace Clima.Core.Scheduler.Network.Messages
{
    public class SchedulerDebugResponse
    {
        public float VentMaxSetPoint { get; set; }
        public float VentMinSetPoint { get; set; }
        public float VentSetPoint { get; set; }
        
        public float ValveCurrent { get; set; }
        public float ValveSetPoint { get; set; }
        
        public float MineCurrent { get; set; }
        public float MineSetPoint { get; set; }
        
        public int CurrentHeads { get; set; }
        public int CurrentState { get; set; }
    }
}