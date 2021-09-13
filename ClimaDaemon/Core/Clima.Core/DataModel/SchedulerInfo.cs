namespace Clima.Core.DataModel
{
    public class SchedulerInfo
    {
        public string TemperatureProfileKey { get; set; } = "";
        public string TemperatureProfileName { get; set; } = "";
        
        public string VentilationProfileKey { get; set; } = "";
        public string VentilationProfileName { get; set; } = "";
        
        public string ValveProfileKey { get; set; } = "";
        public string ValveProfileName { get; set; } = "";
        public string MineProfileKey { get; set; } = "";
        public string MineProfileName { get; set; } = "";
        
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