using System.Collections.Generic;
using Clima.Basics.Services;
using Clima.Basics.Services.Communication;
using Clima.Basics.Services.Communication.Exceptions;
using Clima.Core.DataModel.GraphModel;
using Clima.Core.Network.Messages;
using Clima.Core.Network.Services;

namespace GraphProviderService
{
    public class GraphProviderService : IGraphProviderService, INetworkService
    {
        private readonly IGraphProviderFactory _providerFactory;
        public ISystemLogger Log { get; set; }

        public GraphProviderService(IGraphProviderFactory providerFactory)
        {
            _providerFactory = providerFactory;
        }

        [ServiceMethod]
        public GraphInfosResponse GetTemperatureGraphInfos(GraphInfosRequest request)
        {
            var temperatureProvider = _providerFactory.TemperatureGraphProvider();

            return new GraphInfosResponse()
            {
                GraphType = "Temperature",
                Infos = new List<GraphInfo>(temperatureProvider.GetGraphInfos())
            };
        }

        [ServiceMethod]
        public TemperatureGraphResponse GetTemperatureGraph(GetGraphRequest<TemperatureGraphResponse> request)
        {
            
            if (string.IsNullOrEmpty(request.GraphKey))
                throw new InvalidRequestException("Get temperature graph key is null");
            var tGraph = _providerFactory.TemperatureGraphProvider().GetGraph(request.GraphKey);
            if (tGraph is null)
                throw new KeyNotFoundException(
                    $"key: {request.GraphKey} not contains in temperature graph repository");


            var response = new TemperatureGraphResponse();
            response.Info = tGraph.Info;
            response.Points = new List<ValueByDayPoint>(tGraph.Points);
            return response;
        }
        [ServiceMethod]
        public TemperatureGraphResponse GetCurrentTemperatureGraph(GetGraphRequest<TemperatureGraphResponse> request)
        {
            throw new System.NotImplementedException();
        }

        public string ServiceName { get; } = "GraphProviderService";

        [ServiceMethod]
        public CreateResultRespose CreateTemperatureGraph(CreateGraphRequest request)
        {
            var tGraphProvider = _providerFactory.TemperatureGraphProvider();
            var newKey = "Temp" + tGraphProvider.GetValidKey();
            Log.Debug($"Create temperature graph key:{newKey}");
            var newGraph = tGraphProvider.CreateGraph(newKey);
            newGraph.Info.Key = request.Info.Key;
            newGraph.Info.Name = request.Info.Name;
            newGraph.Info.Description = request.Info.Description;
            newGraph.Info.CreationTime = request.Info.CreationTime;
            newGraph.Info.ModifiedTime = request.Info.ModifiedTime;

            _providerFactory.Save();
            return new CreateResultRespose();
        }

        [ServiceMethod]
        public DefaultResponse UpdateTemperatureGraph(UpdateTemperatureGraphRequest request)
        {
            var tGraphProvider = _providerFactory.TemperatureGraphProvider();
            var graph = tGraphProvider.GetGraph(request.GraphKey);
            if (graph is null)
            {
                return new DefaultResponse()
                    {RequestName = "UpdateTemperatureGraph", Status = $"Graph:{request.GraphKey} not found"};
            }

            return new DefaultResponse()
                {RequestName = "UpdateTemperatureGraph", Status = $"OK"};

        }
        [ServiceMethod]
        public DefaultResponse RemoveTemperatureGraph(RemoveGraphRequest request)
        {
            
            return new DefaultResponse();
        }
        [ServiceMethod]
        public GraphInfosResponse GetVentilationGraphInfos(GraphInfosRequest request)
        {
            throw new System.NotImplementedException();
        }
        [ServiceMethod]
        public VentilationGraphResponse GetVentilationGraph(GetGraphRequest<VentilationGraphResponse> request)
        {
            throw new System.NotImplementedException();
        }
        [ServiceMethod]
        public CreateResultRespose CreateVentilationGraph(CreateGraphRequest request)
        {
            throw new System.NotImplementedException();
        }
        [ServiceMethod]
        public DefaultResponse RemoveVentilationGraph(RemoveGraphRequest request)
        {
            throw new System.NotImplementedException();
        }
        [ServiceMethod]
        public DefaultResponse UpdateVentilationGraph(UpdateVentilationGraphRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}