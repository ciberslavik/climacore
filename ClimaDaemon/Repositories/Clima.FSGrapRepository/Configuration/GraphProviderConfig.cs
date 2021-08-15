using System.Collections.Generic;
using Clima.Basics.Configuration;

namespace Clima.FSGrapRepository.Configuration
{
    public class GraphProviderConfig<TPointConfig> : IConfigurationItem
        where TPointConfig : IGraphPointConfig<TPointConfig>, new()
    {
        public GraphProviderConfig()
        {
        }

        public string CurrentGraph { get; set; }

        public Dictionary<string, GraphConfig<TPointConfig>> Graphs { get; set; } =
            new Dictionary<string, GraphConfig<TPointConfig>>();

        public string ConfigurationName { get; set; }
    }
}