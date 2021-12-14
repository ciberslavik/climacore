using System.Collections.Generic;
using Clima.Core.Controllers.Light;

namespace Clima.Core.Controllers.Network.Messages.Light
{
    public class LightProfileListResponse
    {
        public LightProfileListResponse()
        {
            
        }
        public List<LightTimerProfileInfo> Profiles { get; set; } = new List<LightTimerProfileInfo>();
    }
}