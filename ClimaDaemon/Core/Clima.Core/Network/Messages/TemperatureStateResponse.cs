using System.Collections.Generic;

namespace Clima.Core.Network.Messages
{
    public class TemperatureStateResponse
    {
        public TemperatureStateResponse()
        {
            
        }
        public float FrontTemperature { get; set; }
        public float RearTemperature { get; set; }
        public float OutdoorTemperature { get; set; }
        public float AverageTemperature { get; set; }
        public float TemperatureCorrection { get; set; }
        public string CurrentGraphName { get; set; }
        public List<float> AvgGraphPoints { get; set; } = new List<float>();
    }
}