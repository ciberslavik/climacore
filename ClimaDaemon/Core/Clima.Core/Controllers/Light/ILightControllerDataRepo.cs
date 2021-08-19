using System.Collections.Generic;

namespace Clima.Core.Controllers.Light
{
    public interface ILightControllerDataRepo
    {
       LightTimerPreset GetCurrentPreset();
        void SetCurrentPreset(string presetKey);

        List<LightTimerPreset> GetAllPresets();
        LightTimerPreset GetPreset(string key);
        void AddPreset(LightTimerPreset preset);
        void RemovePreset(LightTimerPreset preset);
        void UpdatePreset(LightTimerPreset preset);
        bool Exist(string key);
        int Count { get; }
    }
}