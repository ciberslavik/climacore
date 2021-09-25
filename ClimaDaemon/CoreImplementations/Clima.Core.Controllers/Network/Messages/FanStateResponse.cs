using Clima.Core.DataModel;

namespace Clima.Core.Controllers.Network.Messages
{
    public class FanStateResponse
    {
        public string Key { get; set; }
        public FanStateEnum State { get; set; }
    }
}