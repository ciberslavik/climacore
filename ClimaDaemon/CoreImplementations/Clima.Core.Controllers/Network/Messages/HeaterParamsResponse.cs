using Clima.Core.DataModel;

namespace Clima.Core.Controllers.Network.Messages
{
    public class HeaterParamsResponse
    {
        public HeaterParamsResponse()
        {
        }
        public HeaterParams Params { get; set; } = new HeaterParams();
    }
}