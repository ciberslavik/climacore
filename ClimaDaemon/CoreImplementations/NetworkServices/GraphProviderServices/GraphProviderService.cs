using System.Collections.Generic;
using Clima.Basics.Services;
using Clima.Basics.Services.Communication;
using Clima.Basics.Services.Communication.Exceptions;
using Clima.Core.DataModel.GraphModel;
using Clima.Core.Network.Messages;
using Clima.Core.Network.Services;

namespace GraphProviderService
{
    public class GraphProviderService : INetworkService
    {
        private readonly IGraphProviderFactory _providerFactory;
        public ISystemLogger Log { get; set; }
        public string ServiceName { get; } = "GraphProviderService";
        public GraphProviderService(IGraphProviderFactory providerFactory)
        {
            _providerFactory = providerFactory;
        }
        
        #region GetMethods
        [ServiceMethod]
        public GraphInfosResponse GetTemperatureProfileInfos(GraphInfosRequest request)
        {
            var temperatureProvider = _providerFactory.TemperatureGraphProvider();

            return new GraphInfosResponse()
            {
                GraphType = (int)GraphType.Temperature,
                Infos = new List<ProfileInfo>(temperatureProvider.GetGraphInfos())
            };
        }
        [ServiceMethod]
        public TemperatureGraphResponse GetTemperatureProfile(GetProfileRequest<TemperatureGraphResponse> request)
        {
            
            if (string.IsNullOrEmpty(request.ProfileKey))
                throw new InvalidRequestException("Get temperature graph key is null");
            var tGraph = _providerFactory.TemperatureGraphProvider().GetGraph(request.ProfileKey);
            
            if (tGraph is null)
                throw new InvalidRequestException(
                    $"key: {request.ProfileKey} not contains in temperature graph repository");

            Log.Debug($"Get temperature profile request:{request.ProfileKey}");
            
            var response = new TemperatureGraphResponse();
            response.Info = tGraph.Info;
            response.Points = new List<ValueByDayPoint>(tGraph.Points);
            return response;
        }

        [ServiceMethod]
        public GraphInfosResponse GetVentilationProfileInfos(GraphInfosRequest request)
        {
            var ventilationProvider = _providerFactory.VentilationGraphProvider();

            return new GraphInfosResponse()
            {
                GraphType = (int)GraphType.Ventilation,
                Infos = new List<ProfileInfo>(ventilationProvider.GetGraphInfos())
            };
        }
        [ServiceMethod]
        public VentilationGraphResponse GetVentilationProfile(GetProfileRequest<VentilationGraphResponse> request)
        {
            if (string.IsNullOrEmpty(request.ProfileKey))
                throw new InvalidRequestException("Get ventilation graph key is null");
            var vGraph = _providerFactory.VentilationGraphProvider().GetGraph(request.ProfileKey);
            
            if (vGraph is null)
                throw new InvalidRequestException(
                    $"key: {request.ProfileKey} not contains in ventilation graph repository");

            Log.Debug($"Get ventilation profile request:{request.ProfileKey}");
            
            var response = new VentilationGraphResponse();
            response.Info = vGraph.Info;
            response.Points = new List<MinMaxByDayPoint>(vGraph.Points);
            return response;
        }
        
        
        [ServiceMethod]
        public GraphInfosResponse GetValveProfileInfos(GraphInfosRequest request)
        {
            var valveProvider = _providerFactory.ValveGraphProvider();

            return new GraphInfosResponse()
            {
                GraphType = (int)GraphType.ValveByVent,
                Infos = new List<ProfileInfo>(valveProvider.GetGraphInfos())
            };
        }
        
        [ServiceMethod]
        public ValveGraphResponse GetValveProfile(GetProfileRequest<ValveGraphResponse> request)
        {
            if (string.IsNullOrEmpty(request.ProfileKey))
                throw new InvalidRequestException("Get valve graph key is null");
            var vGraph = _providerFactory.ValveGraphProvider().GetGraph(request.ProfileKey);
            
            if (vGraph is null)
                throw new InvalidRequestException(
                    $"key: {request.ProfileKey} not contains in valve graph repository");

            Log.Debug($"Get valve profile request:{request.ProfileKey}");
            
            var response = new ValveGraphResponse();
            response.Info = vGraph.Info;
            response.Points = new List<ValueByValuePoint>(vGraph.Points);
            return response;
        }
        #endregion GetMethods
        
        
        
        [ServiceMethod]
        public CreateResultRespose CreateTemperatureProfile(CreateProfileRequest request)
        {
            var tGraphProvider = _providerFactory.TemperatureGraphProvider();
            var newKey = tGraphProvider.GetValidKey();
            Log.Debug($"Create temperature graph key:{newKey}");
            var newGraph = tGraphProvider.CreateGraph(newKey);
            
            newGraph.Info.Name = request.Info.Name;
            newGraph.Info.Description = request.Info.Description;
            newGraph.Info.CreationTime = request.Info.CreationTime;
            newGraph.Info.ModifiedTime = request.Info.ModifiedTime;

            _providerFactory.Save();
            return new CreateResultRespose() {NewGraphKey = newKey};
        }

        [ServiceMethod]
        public DefaultResponse UpdateTemperatureProfile(UpdateTemperatureGraphRequest request)
        {
            var tempGraphProvider = _providerFactory.TemperatureGraphProvider();
            var graph = tempGraphProvider.GetGraph(request.Profile.Info.Key);

            graph.Info = request.Profile.Info;
            graph.Points = request.Profile.Points;
            
            _providerFactory.Save();
            return new DefaultResponse()
                {RequestName = "UpdateTemperatureGraph", Status = $"OK"};

        }
        
        [ServiceMethod]
        public DefaultResponse RemoveTemperatureProfile(RemoveGraphRequest request)
        {
            var tGraphProvider = _providerFactory.TemperatureGraphProvider();
            if (tGraphProvider.ContainsKey(request.Key))
            {
                tGraphProvider.RemoveGraph(request.Key);
                return new DefaultResponse();
            }

            return new DefaultResponse($"Key:{request.Key} not contains in storage");
        }
        
        
        [ServiceMethod]
        public CreateResultRespose CreateVentilationProfile(CreateProfileRequest request)
        {
            var vGraphProvider = _providerFactory.VentilationGraphProvider();
            var newKey = vGraphProvider.GetValidKey();
            Log.Debug($"Create ventilation graph key:{newKey}");
            var newGraph = vGraphProvider.CreateGraph(newKey);
            
            newGraph.Info.Name = request.Info.Name;
            newGraph.Info.Description = request.Info.Description;
            newGraph.Info.CreationTime = request.Info.CreationTime;
            newGraph.Info.ModifiedTime = request.Info.ModifiedTime;

            _providerFactory.Save();
            return new CreateResultRespose() {NewGraphKey = newKey};
        }
        [ServiceMethod]
        public DefaultResponse RemoveVentilationProfile(RemoveGraphRequest request)
        {
            var vGraphProvider = _providerFactory.VentilationGraphProvider();
            if (vGraphProvider.ContainsKey(request.Key))
            {
                vGraphProvider.RemoveGraph(request.Key);
                return new DefaultResponse();
            }

            return new DefaultResponse($"Key:{request.Key} not contains in storage");
        }
        [ServiceMethod]
        public DefaultResponse UpdateVentilationProfile(UpdateVentilationGraphRequest request)
        {
            var ventGraphProvider = _providerFactory.VentilationGraphProvider();
            var graph = ventGraphProvider.GetGraph(request.Profile.Info.Key);

            graph.Info = request.Profile.Info;
            graph.Points = request.Profile.Points;
            
            _providerFactory.Save();
            return new DefaultResponse()
                {RequestName = "UpdateVentilationGraph", Status = $"OK"};
        }

        [ServiceMethod]
        public CreateResultRespose CreateValveProfile(CreateProfileRequest request)
        {
            var vGraphProvider = _providerFactory.ValveGraphProvider();
            var newKey = vGraphProvider.GetValidKey();
            Log.Debug($"Create ventilation graph key:{newKey}");
            var newGraph = vGraphProvider.CreateGraph(newKey);
            
            newGraph.Info.Name = request.Info.Name;
            newGraph.Info.Description = request.Info.Description;
            newGraph.Info.CreationTime = request.Info.CreationTime;
            newGraph.Info.ModifiedTime = request.Info.ModifiedTime;

            _providerFactory.Save();
            return new CreateResultRespose() {NewGraphKey = newKey};
        }
        [ServiceMethod]
        public DefaultResponse UpdateValveProfile(UpdateValveGraphRequest request)
        {
            var valveGraphProvider = _providerFactory.ValveGraphProvider();
            var graph = valveGraphProvider.GetGraph(request.Profile.Info.Key);

            graph.Info = request.Profile.Info;
            graph.Points = request.Profile.Points;
            
            _providerFactory.Save();
            return new DefaultResponse()
                {RequestName = "UpdateValveGraph", Status = $"OK"};
        }

        [ServiceMethod]
        public DefaultResponse RemoveValveProfile(RemoveGraphRequest request)
        {
            var vGraphProvider = _providerFactory.ValveGraphProvider();
            if (vGraphProvider.ContainsKey(request.Key))
            {
                vGraphProvider.RemoveGraph(request.Key);
                _providerFactory.Save();
                return new DefaultResponse();
            }

            return new DefaultResponse($"Key:{request.Key} not contains in storage");
        }
    }
}