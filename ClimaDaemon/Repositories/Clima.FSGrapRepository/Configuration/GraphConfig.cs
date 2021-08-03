using System.Collections.Generic;

namespace Clima.FSGrapRepository.Configuration
{
    public class GraphConfig<TPointConfig>  
        where TPointConfig : IGraphPointConfig<TPointConfig>, new()
    {
        public GraphConfig()
        {
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Key { get; set; }
        public List<TPointConfig> Points { get; set; } = new List<TPointConfig>();
    }
}