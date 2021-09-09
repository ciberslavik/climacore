using System.Collections.Generic;
using Clima.Basics.Configuration;
using Clima.Core.DataModel;

namespace Clima.Core.Controllers.Configuration
{
    public class HeaterControllerConfig:IConfigurationItem
    {
        public HeaterControllerConfig()
        {
        }

        public Dictionary<string, HeaterInfo> Infos { get; set; } = new Dictionary<string, HeaterInfo>();

        public static HeaterControllerConfig CreateDefault()
        {
            var result = new HeaterControllerConfig();
            result.Infos.Add("HEAT:0",new HeaterInfo()
            {
                Key = "HEAT:0",
                ControlZone = 0,
                Hysteresis = 1,
                IsManual = false,
                ManualSetPoint = 26,
                PinName = "DO:3:10"
            });
            result.Infos.Add("HEAT:1",new HeaterInfo()
            {
                Key = "HEAT:1",
                ControlZone = 1,
                Hysteresis = 1,
                IsManual = false,
                ManualSetPoint = 26,
                PinName = "DO:3:11"
            });

            return result;
        }

        public string ConfigurationName => FileName;
        public static string FileName = "HeaterController";
    }
}