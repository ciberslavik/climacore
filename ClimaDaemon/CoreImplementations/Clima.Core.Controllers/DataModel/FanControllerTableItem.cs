using Clima.Core.DataModel;
using Clima.Core.Devices;

namespace Clima.Core.Conrollers.Ventilation.DataModel
{
    internal class FanControllerTableItem
    {
        internal FanControllerTableItem()
        {
        }

        internal float StartPerformance { get; set; }
        internal float StopPerformance { get; set; }
        internal float CurrentPerformance { get; set; }
        internal int FanId { get; set; }
        internal int Priority { get; set; }
        internal bool IsDead { get; set; }
        internal bool IsRunning { get; set; }
        internal bool IsHermetise { get; set; }
        internal bool IsManual { get; set; }
        internal bool IsAnalog { get; set; }
        internal IRelay Relay { get; set; }
        internal IFrequencyConverter AnalogFan { get; set; }
        internal FanInfo Info { get; set; }
    }
}