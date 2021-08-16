using Clima.Core.Network.Messages;

namespace Clima.Core.Network.Services
{
    public interface IGraphProviderService
    {
        GraphInfosResponse GetTemperatureGraphInfos(GraphInfosRequest request);
        TemperatureGraphResponse GetTemperatureGraph(GetGraphRequest<TemperatureGraphResponse> request);
        CreateResultRespose CreateGraph(CreateGraphRequest request);
    }
}