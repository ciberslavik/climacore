namespace Clima.Core.DataModel
{
    public class SchedulerProcessInfo
    {
        public float TemperatureSetPoint { get; set; }
        public float VentilationMaxPoint { get; set; }
        public float VentilationMinPoint { get; set; }
        public float VentilationSetPoint { get; set; }
        public float ValveSetPoint { get; set; }
        public float MineSetPoint { get; set; }
        
        public int CurrentDay { get; set; }
        public int CurrentHeads { get; set; }
        
    }
}