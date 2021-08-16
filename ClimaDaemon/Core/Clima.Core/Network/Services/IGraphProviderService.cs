using Clima.Core.Network.Messages;

namespace Clima.Core.Network.Services
{
    public interface IGraphProviderService
    {
        GraphInfosResponse GetTemperatureGraphInfos(GraphInfosRequest request);
        TemperatureGraphResponse GetTemperatureGraph(GetGraphRequest<TemperatureGraphResponse> request);
        CreateResultRespose CreateTemperatureGraph(CreateGraphRequest request);
        DefaultResponse RemoveTemperatureGraph(RemoveGraphRequest request);

        GraphInfosResponse GetVentilationGraphInfos(GraphInfosRequest request);
        

    }
}