using System.Collections.Generic;
using Clima.Core.DataModel.GraphModel;

namespace Clima.Core.Network.Messages
{
    public class GraphInfosResponse
    {
        public GraphInfosResponse()
        {
        }
        public string GraphType { get; set; }
        public List<ProfileInfo> Infos { get; set; } = new List<ProfileInfo>();
    }
}