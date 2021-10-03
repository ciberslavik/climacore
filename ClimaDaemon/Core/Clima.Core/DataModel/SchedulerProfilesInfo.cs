namespace Clima.Core.DataModel
{
    public class SchedulerProfilesInfo
    {
        public SchedulerProfilesInfo()
        {
            
        }
        public string TemperatureProfileKey { get; set; } = "";
        public string TemperatureProfileName { get; set; } = "";
        
        public string VentilationProfileKey { get; set; } = "";
        public string VentilationProfileName { get; set; } = "";
        
        public string ValveProfileKey { get; set; } = "";
        public string ValveProfileName { get; set; } = "";
        public string MineProfileKey { get; set; } = "";
        public string MineProfileName { get; set; } = "";
    }
}