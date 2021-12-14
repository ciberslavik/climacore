using System.Collections.Generic;
using Clima.Basics.Services;

namespace Clima.Core.Controllers.Light
{
    public interface ILightController
    {
        void ProcessLight();
        void SetCurrentProfileKey(string profileKey);
        LightTimerProfile CurrentProfile { get; }
        LightTimerProfile CreateProfile(string profileName);
        void UpdateProfile(LightTimerProfile profile);
        void RemoveProfile(string profileKey);
        
        Dictionary<string,LightTimerProfile> Profiles { get; }
    }
}