using System;

namespace Clima.Core.Scheduler
{
    public class ClimaScheduler : IClimaScheduler
    {
        public bool Process()
        {
            throw new NotImplementedException();
        }

        public void SetTemperatureGraph(string graphKey)
        {
            throw new NotImplementedException();
        }

        public ShedulerStateObject SchedulerState { get; }
    }
}