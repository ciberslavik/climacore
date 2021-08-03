namespace Clima.Core.DataModel.GraphModel
{
    public interface IGraphProviderFactory
    {
        IGraphProvider<TemperatureGraph> TemperatureGraphProvider();
        IGraphProvider<VentilationMinMaxGraph> VentilationMinMaxGraphProvider();
        IGraphProvider<ValvePerVentilationGraph> ValvePerVentilationGraphProvider();
    }
}