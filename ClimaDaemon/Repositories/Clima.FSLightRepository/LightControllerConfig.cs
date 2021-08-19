using System.Collections.Generic;
using Clima.Basics.Configuration;
using Clima.Core.Controllers.Light;

namespace Clima.FSLightRepository
{
    public class LightControllerConfig:IConfigurationItem
    {
        public LightControllerConfig()
        {
        }

        public Dictionary<string, LightTimerPreset> Presets { get; set; } = new Dictionary<string, LightTimerPreset>();
        public string CurrentPresetKey { get; set; }
        public string ConfigurationName => FileName;
        private const string FileName = "LightControllerConfig";

        public static LightControllerConfig CreateDefault()
        {
            var config = new LightControllerConfig();
            config.CurrentPresetKey = "";
            return config;
        }
    }
}