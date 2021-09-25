using Clima.Core.DataModel;

namespace Clima.Core.Controllers.Network.Messages
{
    public class FanModeRequest
    {
        public string Key { get; set; }
        public FanModeEnum Mode { get; set; }
    }
}