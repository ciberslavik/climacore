using System.Collections.Generic;
using Clima.Basics.Services;
using Clima.Core.DataModel.GraphModel;
using Clima.Core.Network.Messages;

namespace Clima.Core.Network.Services
{
    public class GraphProviderService:IGraphProviderService
    {
        private readonly IGraphProviderFactory _providerFactory;
        public ISystemLogger Log { get; set; }
        public GraphProviderService(IGraphProviderFactory providerFactory)
        {
            _providerFactory = providerFactory;
        }

        public GraphInfosResponse GetTemperatureGraphInfos(GraphInfosRequest request)
        {
            var temperatureProvider = _providerFactory.TemperatureGraphProvider();

            return new GraphInfosResponse()
            {
                Infos = new List<GraphInfo>(temperatureProvider.GetGraphInfos())
            };
        }

        public TemperatureGraphResponse GetTemperatureGraph(GetGraphRequest<TemperatureGraphResponse> request)
        {
            return new TemperatureGraphResponse();
        }
    }
}