namespace Clima.Core.Sheduler
{
    public interface IClimaSheduler
    {
        void Process();
        void SetTemperatureGraph();
        ShedulerStateObject ShedulerState { get; }
    }
}