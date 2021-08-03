using System.Collections.Generic;

namespace Clima.FSGrapRepository.Configuration
{
    public class GraphProviderConfig<TPointConfig> 
        where TPointConfig : IGraphPointConfig<TPointConfig>, new()
    {
        private GraphConfig<TPointConfig> _currentGraph;
        
        public GraphProviderConfig()
        {
            
        }
        public GraphConfig<TPointConfig> CurrentGraph { get; set; }
        public List<GraphConfig<TPointConfig>> Graphs { get; set; } = new List<GraphConfig<TPointConfig>>();
    }
}