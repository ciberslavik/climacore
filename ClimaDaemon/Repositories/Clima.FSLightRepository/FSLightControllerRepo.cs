using System;
using System.Collections.Generic;
using Clima.Basics.Configuration;
using Clima.Core.Controllers.Light;

namespace Clima.FSLightRepository
{
    public class FSLightControllerRepo:ILightControllerDataRepo
    {
        private readonly IConfigurationStorage _configStore;

        public FSLightControllerRepo(IConfigurationStorage configStore)
        {
            _configStore = configStore;
        }
        
        public LightTimerPreset GetCurrentPreset()
        {
            throw new NotImplementedException();
        }

        public void SetCurrentPreset(LightTimerPreset preset)
        {
            throw new NotImplementedException();
        }

        public List<LightTimerPreset> GetAllPresets()
        {
            throw new NotImplementedException();
        }

        public LightTimerPreset GetPreset(string key)
        {
            throw new NotImplementedException();
        }

        public void AddPreset(LightTimerPreset preset)
        {
            throw new NotImplementedException();
        }

        public void RemovePreset(LightTimerPreset preset)
        {
            throw new NotImplementedException();
        }

        public void UpdatePreset(LightTimerPreset preset)
        {
            throw new NotImplementedException();
        }
    }
}