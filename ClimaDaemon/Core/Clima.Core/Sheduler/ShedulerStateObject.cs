using System;
using Clima.Core.DataModel.GraphModel;

namespace Clima.Core.Scheduler
{
    public class ShedulerStateObject
    {
        public ShedulerStateObject()
        {
        }

        public SchedulerState State { get; set; }
        
        public float TemperatureSetPoint { get; set; }
        public float VentilationMax { get; set; }
        public float VentilationMin { get; set; }
        public DateTime StartGrowingTime { get; set;}
        
        public GraphBase<ValueByDayPoint> TemperatureGraph { get; set; }
        public GraphBase<MinMaxByDayPoint> VentilationMinMaxGraph { get; set; }
        
    }
}