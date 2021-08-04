﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Clima.Basics.Services;
using Clima.Core.DataModel.GraphModel;
using Clima.Core.Exceptions;
using Clima.Core.Network.Messages;
using Clima.NetworkServer.Exceptions;

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
    }
}