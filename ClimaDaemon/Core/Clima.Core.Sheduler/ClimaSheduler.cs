using System;

namespace Clima.Core.Sheduler
{
    public class ClimaSheduler : IClimaSheduler
    {
        public void Process()
        {
            throw new NotImplementedException();
        }

        public void SetTemperatureGraph(string graphKey)
        {
            throw new NotImplementedException();
        }

        public ShedulerStateObject ShedulerState { get; }
    }
}