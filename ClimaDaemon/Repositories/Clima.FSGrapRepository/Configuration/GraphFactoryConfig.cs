using Clima.Basics.Configuration;

namespace Clima.FSGrapRepository.Configuration
{
    public class GraphFactoryConfig:IConfigurationItem
    {
        public GraphFactoryConfig()
        {
        }

        public GraphConfig<TemperatureGraphPointConfig> TemperatureGraphConfig { get; set; } = new GraphConfig<TemperatureGraphPointConfig>();

        public string ConfigurationName { get; }
        public const string FileName = "GraphFactoryData";
    }
}