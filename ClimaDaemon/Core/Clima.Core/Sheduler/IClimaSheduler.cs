namespace Clima.Core.Sheduler
{
    public interface IClimaSheduler
    {
        void Process();
        void SetTemperatureGraph(string graphKey);
        ShedulerStateObject ShedulerState { get; }
    }
}