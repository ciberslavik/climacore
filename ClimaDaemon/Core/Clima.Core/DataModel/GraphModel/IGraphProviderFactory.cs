namespace Clima.Core.DataModel.GraphModel
{
    public interface IGraphProviderFactory
    {
        IGraphProvider<TemperatureGraph, ValueByDayPoint> TemperatureGraphProvider();
        IGraphProvider<VentilationMinMaxGraph, MinMaxByDayPoint> VentilationMinMaxGraphProvider();
        IGraphProvider<ValvePerVentilationGraph, ValueByValuePoint> ValvePerVentilationGraphProvider();
    }
}