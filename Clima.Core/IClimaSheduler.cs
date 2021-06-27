using Clima.DataModel.Process;

namespace Clima.Core
{
    public interface IClimaSheduler
    {
        void StartSheduler();
        void StopSheduler();
        int ControlCycleTime { get; set; }
        int MeasureCycleTime { get; set; }
        ShedulerContext Context { get; set; }
    }
}