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

        public Dictionary<string, HeaterParams> Infos { get; set; } = new Dictionary<string, HeaterParams>();

        public static HeaterControllerConfig CreateDefault()
        {
            var result = new HeaterControllerConfig();
            result.Infos.Add("HEAT:0",new HeaterParams()
            {
                Key = "HEAT:0",
                ControlZone = 0,
                DeltaOn = -1,
                DeltaOff = -0.5f,
                IsManual = false,
                ManualSetPoint = 26,
                PinName = "DO:3:10",
                Correction = 0
            });
            result.Infos.Add("HEAT:1",new HeaterParams()
            {
                Key = "HEAT:1",
                ControlZone = 1,
                DeltaOn = -1,
                DeltaOff = -0.5f,
                IsManual = false,
                ManualSetPoint = 26,
                PinName = "DO:3:11",
                Correction = 0
            });

            return result;
        }

        public string ConfigurationName => FileName;
        public static string FileName = "HeaterController";
    }
}