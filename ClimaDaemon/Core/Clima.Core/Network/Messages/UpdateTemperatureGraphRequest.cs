using Clima.Basics.Services.Communication;

namespace Clima.Core.Network.Messages
{
    public class UpdateTemperatureGraphRequest:IReturn<DefaultResponse>
    {
        public UpdateTemperatureGraphRequest()
        {
        }
        public string GraphKey { get; set; }
    }
}