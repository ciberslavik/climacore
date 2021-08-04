using System.Collections.Generic;
using Clima.Core.DataModel.GraphModel;

namespace Clima.Core.Network.Messages
{
    public class TemperatureGraphResponse
    {
        public TemperatureGraphResponse()
        {
        }

        public GraphInfo Info { get; set; } = new GraphInfo();
        public List<ValueByDayPoint> Points { get; set; } = new List<ValueByDayPoint>();
    }
}