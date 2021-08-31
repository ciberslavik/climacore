using Clima.Core.DataModel;

namespace Clima.Core.Controllers.Network.Messages
{
    public class CreateFanResponse
    {
        public CreateFanResponse()
        {
        }

        public FanInfo NewFanInfo { get; set; }
    }
}