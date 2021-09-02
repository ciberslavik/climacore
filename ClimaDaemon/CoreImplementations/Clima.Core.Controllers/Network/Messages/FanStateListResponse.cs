using System.Collections.Generic;
using Clima.Core.DataModel;

namespace Clima.Core.Controllers.Network.Messages
{
    public class FanStateListResponse
    {
        public FanStateListResponse()
        {
        }

        public List<FanState> States { get; set; }= new List<FanState>();
    }
}