using System.Collections.Generic;
using Clima.Core.DataModel.GraphModel;

namespace Clima.Core.Network.Messages
{
    public class ValveGraphResponse
    {
        public ValveGraphResponse()
        {
            
        }
        public ProfileInfo Info { get; set; } = new ProfileInfo();
        public List<ValueByValuePoint> Points { get; set; } = new List<ValueByValuePoint>();
    }
}