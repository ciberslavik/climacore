using System.Collections.Generic;
using Clima.Core.DataModel.GraphModel;

namespace Clima.Core.Network.Messages
{
    public class TemperatureGraphResponse
    {
        public TemperatureGraphResponse()
        {
        }

        public ProfileInfo Info { get; set; } = new ProfileInfo();
        public List<ValueByDayPoint> Points { get; set; } = new List<ValueByDayPoint>();
    }
}