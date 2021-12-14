using Clima.Core.Controllers.Light;

namespace Clima.Core.Controllers.Network.Messages.Light
{
    public class LightProfileResponse
    {
        public LightProfileResponse()
        {
            
        }
        public LightProfileResponse(LightTimerProfile profile)
        {
            Profile = profile;
        }
        public LightTimerProfile Profile { get; set; } = new LightTimerProfile();
    }
}