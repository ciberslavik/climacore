using System.Collections.Generic;
using Clima.Core.Controllers.Heater;
using Clima.Core.DataModel;

namespace Clima.Core.Controllers.Network.Messages
{
    public class HeaterStateListResponse
    {
        public HeaterStateListResponse()
        {
        }

        public List<HeaterState> States { get; set; } = new List<HeaterState>();
    }
}