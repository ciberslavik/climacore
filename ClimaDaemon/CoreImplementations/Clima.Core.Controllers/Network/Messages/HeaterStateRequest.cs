using Clima.Core.Controllers.Heater;
using Clima.Core.DataModel;

namespace Clima.Core.Controllers.Network.Messages
{
    public class HeaterStateRequest
    {
        public HeaterStateRequest()
        {
        }

        public string Key { get; set; }
        public HeaterState State { get; set; }=new HeaterState();
    }
}