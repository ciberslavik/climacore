using Clima.Core.DataModel;

namespace Clima.Core.Controllers.Network.Messages
{
    public class HeaterInfoRequest
    {
        public HeaterInfoRequest()
        {
        }

        public HeaterInfo Info { get; set; } = new HeaterInfo();
    }
}