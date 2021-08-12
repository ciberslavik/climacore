using System.Collections.Generic;
using Clima.Core.DataModel.GraphModel;

namespace Clima.FSGrapRepository.Configuration
{
    public class GraphConfig<TPointConfig>
        where TPointConfig : IGraphPointConfig<TPointConfig>, new()
    {
        private List<TPointConfig> _points = new List<TPointConfig>();

        public GraphConfig()
        {
        }

        public GraphInfo Info { get; set; } = new GraphInfo();

        public List<TPointConfig> Points
        {
            get => _points;
            set => _points = value;
        }
    }
}