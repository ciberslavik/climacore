namespace Clima.Core.Scheduler
{
    public interface IClimaScheduler
    {
        bool Process();
        void SetTemperatureGraph(string graphKey);
        ShedulerStateObject SchedulerState { get; }
    }
}