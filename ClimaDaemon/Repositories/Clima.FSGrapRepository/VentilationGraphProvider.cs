using System.Linq;
using Clima.Core.DataModel.GraphModel;
using Clima.FSGrapRepository.Configuration;

namespace Clima.FSGrapRepository
{
    public class VentilationGraphProvider:GraphProviderBase<VentilationGraph, MinMaxByDayPoint,
        VentilationGraphPointConfig>
    {
        public VentilationGraphProvider(GraphProviderConfig<VentilationGraphPointConfig> config) : base(config)
        {
        }

        protected override VentilationGraph CreateFromConfig(GraphConfig<VentilationGraphPointConfig> config)
        {
            var graph = new VentilationGraph()
            {
                Info = config.Info
            };

            foreach (var point in config.Points.Select(pointConfig =>
                new MinMaxByDayPoint(pointConfig.Day, pointConfig.MinValue, pointConfig.MaxValue))) graph.Points.Add(point);

            return graph;
        }

        protected override GraphConfig<VentilationGraphPointConfig> PopulateConfigFromGraph(
            ref GraphConfig<VentilationGraphPointConfig> config, VentilationGraph graph)
        {
            config.Info = graph.Info;
            config.Points.Clear();
            foreach (var point in graph.Points)
            {
                var pointConfig = new VentilationGraphPointConfig(point.Day, point.MinValue, point.MaxValue);
                config.Points.Add(pointConfig);
            }

            return config;
        }
    }
}