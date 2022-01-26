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
        internal IRelay Relay { get; set; }
        internal IFrequencyConverter AnalogFan { get; set; }
        internal FanInfo Info { get; set; }
        internal FanStateEnum PreAlarmState { get; set; }
    }
}