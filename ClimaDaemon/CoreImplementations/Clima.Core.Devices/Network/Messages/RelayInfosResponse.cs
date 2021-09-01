using System.Collections.Generic;
using Clima.Core.DataModel;

namespace Clima.Core.Devices.Network.Messages
{
    public class RelayInfosResponse
    {
        public RelayInfosResponse()
        {
            
        }

        public List<RelayInfo> Infos { get; set; } = new List<RelayInfo>();
    }
}