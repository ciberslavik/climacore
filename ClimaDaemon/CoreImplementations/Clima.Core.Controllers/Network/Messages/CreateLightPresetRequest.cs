using Clima.Basics.Services.Communication;
using Clima.Core.Network.Messages;

namespace Clima.Core.Controllers.Network.Messages
{
    public class CreateLightPresetRequest:IReturn<DefaultResponse>
    {
        public CreateLightPresetRequest()
        {
        }
        public string PresetName { get; set; }
        
    }
}