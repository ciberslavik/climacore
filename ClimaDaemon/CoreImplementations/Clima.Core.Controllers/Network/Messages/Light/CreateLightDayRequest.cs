using Clima.Basics.Services.Communication;
using Clima.Core.Network.Messages;

namespace Clima.Core.Controllers.Network.Messages.Light
{
    public class CreateLightDayRequest:IReturn<DefaultResponse>
    {
        public CreateLightDayRequest()
        {
        }
        public string PresetKey { get; set; }
        public int Day { get; set; }
    }
}