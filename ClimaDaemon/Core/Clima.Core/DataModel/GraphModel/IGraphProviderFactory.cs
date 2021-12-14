namespace Clima.Core.DataModel.GraphModel
{
    public interface IGraphProviderFactory
    {
        IGraphProvider<TemperatureGraph, ValueByDayPoint> TemperatureGraphProvider();
        IGraphProvider<VentilationGraph, MinMaxByDayPoint> VentilationGraphProvider();
        IGraphProvider<ValvePerVentilationGraph, ValueByValuePoint> ValveGraphProvider();
        IGraphProvider<ValvePerVentilationGraph, ValueByValuePoint> MineGraphProvider();
        IGraphProvider<VentCorrectionGraph, ValueByValuePoint> VentCorrectionGraphProvider();

        void Save();
    }
}