using System.Collections.Generic;

namespace Clima.Core.Network.Messages
{
    public class TemperatureStateResponse
    {
        public TemperatureStateResponse()
        {
            
        }

        public float FrontTemperature { get; set; } = 0.0f;
        public float RearTemperature { get; set; } = 0.0f;
        public float OutdoorTemperature { get; set; } = 0.0f;
        public float AverageTemperature { get; set; } = 0.0f;
        public float TemperatureCorrection { get; set; } = 0.0f;
        public string CurrentGraphName { get; set; } = "";
        public List<float> AvgGraphPoints { get; set; } = new List<float>();
    }
}