using System.Collections.Generic;
using Clima.Core.DataModel.GraphModel;

namespace Clima.Core.Network.Messages
{
    public class GraphInfosResponse
    {
        public GraphInfosResponse()
        {
        }
        public List<GraphInfo> Infos { get; set; }
        
    }
}