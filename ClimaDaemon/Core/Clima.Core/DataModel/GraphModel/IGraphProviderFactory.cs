namespace Clima.Core.DataModel.GraphModel
{
    public interface IGraphProviderFactory
    {
        IGraphProvider<TemperatureGraph, ValueByDayPoint> TemperatureGraphProvider();
        IGraphProvider<VentilationGraph, MinMaxByDayPoint> VentilationGraphProvider();
        IGraphProvider<ValvePerVentilationGraph, ValueByValuePoint> ValveGraphProvider();
        void Save();
    }
}