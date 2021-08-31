using Clima.Basics.Configuration;

namespace Clima.Core.Conrollers.Ventilation.Configuration
{
    public class VentilationControllerConfig:IConfigurationItem
    {
        public VentilationControllerConfig()
        {
        }
        

        public string ConfigurationName => FileName;
        public const string FileName = "VentilationConfig";
    }
}