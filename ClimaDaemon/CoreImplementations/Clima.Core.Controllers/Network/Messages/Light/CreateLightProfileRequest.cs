using Clima.Basics.Services.Communication;
using Clima.Core.Network.Messages;

namespace Clima.Core.Controllers.Network.Messages.Light
{
    public class CreateLightProfileRequest:IReturn<DefaultResponse>
    {
        public CreateLightProfileRequest()
        {
        }
        public string ProfileName { get; set; }
        public string Description { get; set; }
    }
}