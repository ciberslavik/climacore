namespace Clima.Core.Ventelation
{
    public class DFanConfig
    {
        public DFanConfig()
        {
        }

        public double PerformancePerFan { get; set; } = 1;
        public int FanCount { get; set; } = 1;
        /// <summary>
        /// Точка запуска, задается в процентах от TotalPerformance
        /// </summary>
        public double StartPower { get; set; }
        /// <summary>
        /// Точка остановки, задается в процентах от TotalPerformance
        /// </summary>
        public double StopPower { get; set; }

        public string RelayName { get; set; } = "REL:0";
        
        public double TotalPerformance
        {
            get => PerformancePerFan * FanCount;
        }
        public  bool IsDiscard { get; set; }
    }
}