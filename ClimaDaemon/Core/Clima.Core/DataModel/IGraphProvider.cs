namespace Clima.Core.DataModel.Graphs
{
    public interface IGraphProvider
    {
        TemperatureGraph GetTemperatureGraph(string graphName);
    }
}