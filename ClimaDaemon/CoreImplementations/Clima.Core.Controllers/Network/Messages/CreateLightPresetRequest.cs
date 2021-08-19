using Clima.Basics.Services.Communication;

namespace Clima.Core.Network.Messages
{
    public class CreateLightPresetRequest:IReturn<DefaultResponse>
    {
        public CreateLightPresetRequest()
        {
        }
        public string PresetName { get; set; }
        
    }
}