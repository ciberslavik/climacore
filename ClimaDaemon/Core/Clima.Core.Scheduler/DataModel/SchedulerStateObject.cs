using System;

namespace Clima.Core.Scheduler.DataModel
{
    internal class SchedulerStateObject
    {
        internal SchedulerStateObject()
        {
            
        }
        
        internal float TemperatureSetPoint { get; set; }
        internal float VentilationMaxPoint { get; set; }
        internal float VentilationMinPoint { get; set; }
        internal float VentilationSetPoint { get; set; }
        internal float VentilationInMeters { get; set; }
        internal float ValveSetPoint { get; set; }
        internal float MaineSetPoint { get; set; }
        internal int CurrentDay { get; set; }
        internal int CurrentHeads { get; set; }
        internal SchedulerState SchedulerState { get; set; }
        
        internal DateTime StartPreparingDate { get; set; }
        internal DateTime StartProductionDate { get; set; }
        internal DateTime StartPreProductionDate { get; set; }
        
    }
}