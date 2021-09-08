using Clima.Core.DataModel;

namespace Clima.Core.Controllers.Network.Messages
{
    public class HeaterInfoResponse
    {
        public HeaterInfoResponse()
        {
        }

        public HeaterInfo Info { get; set; } = new HeaterInfo();
    }
}