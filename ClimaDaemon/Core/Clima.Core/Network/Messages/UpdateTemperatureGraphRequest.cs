using System.Collections.Generic;
using Clima.Basics.Services.Communication;
using Clima.Core.DataModel.GraphModel;

namespace Clima.Core.Network.Messages
{
    public class UpdateTemperatureGraphRequest:IReturn<DefaultResponse>
    {
        public UpdateTemperatureGraphRequest(string graphKey="")
        {
            GraphKey = graphKey;
        }
        public string GraphKey { get; set; }
        public GraphInfo Info { get; set; } = new GraphInfo();
        public List<ValueByDayPoint> Points { get; set; } = new List<ValueByDayPoint>();
    }
}