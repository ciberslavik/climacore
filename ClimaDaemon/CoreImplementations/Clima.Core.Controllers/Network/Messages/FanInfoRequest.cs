using Clima.Basics.Services.Communication;
using Clima.Core.DataModel;

namespace Clima.Core.Controllers.Network.Messages
{
    public class FanInfoRequest
    {
        public FanInfoRequest()
        {
        }
        public string FanKey { get; set; }
        public FanInfo Info { get; set; } = new FanInfo(); 
    }
}