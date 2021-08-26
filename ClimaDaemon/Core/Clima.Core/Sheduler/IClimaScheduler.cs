using Clima.Basics.Services;
using Clima.Core.DataModel.GraphModel;

namespace Clima.Core.Scheduler
{
    public interface IClimaScheduler : IService
    {
        void SetTemperatureGraph(GraphBase<ValueByDayPoint> graph);
        void SetVentilationGraph(GraphBase<MinMaxByDayPoint> graph);
        void SetValveGraph(GraphBase<ValueByValuePoint> graph);
        ShedulerStateObject SchedulerState { get; }
    }
}