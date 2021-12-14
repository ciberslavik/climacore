using System;

namespace Clima.Core.Scheduler.DataModel
{
    internal class SchedulerContext
    {
        internal SchedulerContext()
        {
            SetPoints = new SchedulerSetPoints();
        }
        internal SchedulerSetPoints SetPoints { get; set; }
        internal int CurrentDay { get; set; }
        internal int CurrentHeads { get; set; }
        internal SchedulerState State { get; set; }
        internal DateTime StartPreparingDate { get; set; }
        internal DateTime StartProductionDate { get; set; }
        internal DateTime StartPreProductionDate { get; set; }
        
    }
}