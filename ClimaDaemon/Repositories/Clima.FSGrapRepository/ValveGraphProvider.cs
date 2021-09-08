using System.Linq;
using Clima.Core.DataModel.GraphModel;
using Clima.FSGrapRepository.Configuration;

namespace Clima.FSGrapRepository
{
    public class ValveGraphProvider:GraphProviderBase<ValvePerVentilationGraph, ValueByValuePoint,
        ValveGraphPointConfig>
    {
        
        public ValveGraphProvider(GraphProviderConfig<ValveGraphPointConfig> config) : base(config)
        {
        }

        protected override ValvePerVentilationGraph CreateFromConfig(GraphConfig<ValveGraphPointConfig> config)
        {
            var graph = new ValvePerVentilationGraph()
            {
                Info = config.Info
            };

            foreach (var point in config.Points.Select(pointConfig =>
                new ValueByValuePoint(pointConfig.Ventilation, pointConfig.Valve))) graph.Points.Add(point);

            return graph;
        }

        protected override GraphConfig<ValveGraphPointConfig> PopulateConfigFromGraph(ref GraphConfig<ValveGraphPointConfig> config, ValvePerVentilationGraph graph)
        {
            config.Info = graph.Info;
            config.Points.Clear();
            foreach (var point in graph.Points)
            {
                var pointConfig = new ValveGraphPointConfig(point.ValueX, point.ValueY);
                config.Points.Add(pointConfig);
            }

            return config;
        }
    }
}