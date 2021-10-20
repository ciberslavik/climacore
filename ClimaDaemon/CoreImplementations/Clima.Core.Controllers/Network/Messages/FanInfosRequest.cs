using System.Collections.Generic;
using Clima.Core.DataModel;

namespace Clima.Core.Controllers.Network.Messages
{
    public class FanInfosRequest
    {
        public FanInfosRequest()
        {
            
        }
        public Dictionary<string, FanInfo> Infos { get; set; } = new Dictionary<string, FanInfo>();
    }
}