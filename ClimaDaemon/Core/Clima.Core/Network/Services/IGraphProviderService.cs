using Clima.Core.Network.Messages;

namespace Clima.Core.Network.Services
{
    public interface IGraphProviderService
    {
        GraphInfosResponse GetTemperatureGraphInfos(GraphInfosRequest request);
        TemperatureGraphResponse GetTemperatureGraph(GetGraphRequest<TemperatureGraphResponse> request);
        TemperatureGraphResponse GetCurrentTemperatureGraph(GetGraphRequest<TemperatureGraphResponse> request);
        CreateResultRespose CreateTemperatureGraph(CreateGraphRequest request);
        DefaultResponse RemoveTemperatureGraph(RemoveGraphRequest request);
        DefaultResponse UpdateTemperatureGraph(UpdateTemperatureGraphRequest request);
        GraphInfosResponse GetVentilationGraphInfos(GraphInfosRequest request);
        VentilationGraphResponse GetVentilationGraph(GetGraphRequest<VentilationGraphResponse> request);
        CreateResultRespose CreateVentilationGraph(CreateGraphRequest request);
        DefaultResponse RemoveVentilationGraph(RemoveGraphRequest request);
        DefaultResponse UpdateVentilationGraph(UpdateVentilationGraphRequest request);
    }
}