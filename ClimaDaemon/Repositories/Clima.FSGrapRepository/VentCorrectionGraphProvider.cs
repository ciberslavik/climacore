using System.Linq;
using Clima.Core.DataModel.GraphModel;
using Clima.FSGrapRepository.Configuration;

namespace Clima.FSGrapRepository
{
    public class VentCorrectionGraphProvider:GraphProviderBase<VentCorrectionGraph, ValueByValuePoint, VentCorrectionGraphPointConfig>
    {
        public VentCorrectionGraphProvider(GraphProviderConfig<VentCorrectionGraphPointConfig> config) : base(config)
        {
        }

        protected override VentCorrectionGraph CreateFromConfig(GraphConfig<VentCorrectionGraphPointConfig> config)
        {
            var graph = new VentCorrectionGraph()
            {
                Info = config.Info
            };

            foreach (var point in config.Points.Select(pointConfig =>
                new ValueByValuePoint(pointConfig.OutdoorTemp, pointConfig.CorrectionValue))) graph.Points.Add(point);

            return graph;
        }

        protected override GraphConfig<VentCorrectionGraphPointConfig> PopulateConfigFromGraph(ref GraphConfig<VentCorrectionGraphPointConfig> config, VentCorrectionGraph graph)
        {
            config.Info = graph.Info;
            config.Points.Clear();
            foreach (var point in graph.Points)
            {
                var pointConfig = new VentCorrectionGraphPointConfig(point.ValueX, point.ValueY);
                config.Points.Add(pointConfig);
            }

            return config;
        }
    }
}