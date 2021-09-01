using Clima.Core.Devices;

namespace Clima.Core.Controllers.Network.Messages
{
    public class FanStateResponse
    {
        public FanStateResponse()
        {
        }

        public FanState State{get;set;}
    }
}