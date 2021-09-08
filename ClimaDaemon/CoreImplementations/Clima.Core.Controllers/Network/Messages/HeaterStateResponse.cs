using Clima.Core.Controllers.Heater;
using Clima.Core.DataModel;

namespace Clima.Core.Controllers.Network.Messages
{
    public class HeaterStateResponse
    {
        public HeaterStateResponse()
        {
        }

        public HeaterState State { get; set; } = new HeaterState();
    }
}