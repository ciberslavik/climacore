using Clima.Core.DataModel;

namespace Clima.Core.Controllers.Network.Messages
{
    public class FanStateRequest
    {
        public FanStateRequest()
        {
        }

        public string RequestFanKey { get; set; }
        
        public FanState State { get; set; } = new FanState();
    }
}