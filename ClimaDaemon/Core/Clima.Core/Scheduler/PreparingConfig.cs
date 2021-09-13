using System;

namespace Clima.Core.Scheduler
{
    public class PreparingConfig
    {
        public float TemperatureSetPoint { get; set; }
        public float VentilationSetPint { get; set; }
        public float ValveSetPoint { get; set; }
        public float MineSetPoint { get; set; }
        public DateTime StartDate { get; set; }
    }
}