using Clima.Core.DataModel;

namespace Clima.Core.Controllers.Network.Messages
{
    public class FanModeResponse
    {
        public string Key { get; set; }
        public FanModeEnum Mode { get; set; }
    }
}