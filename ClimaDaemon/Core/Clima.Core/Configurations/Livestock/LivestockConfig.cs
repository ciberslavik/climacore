using Clima.Basics.Configuration;

namespace Clima.Core.Configurations.Livestock
{
    public class LivestockConfig:IConfigurationItem
    {
        
        public string ConfigurationName => FileName;
        public const string FileName = "Livestock";
    }
}