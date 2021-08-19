using System;
using System.Collections.Generic;
using System.Linq;
using Clima.Basics.Configuration;
using Clima.Core.Controllers.Light;

namespace Clima.FSLightRepository
{
    public class FSLightControllerRepo : ILightControllerDataRepo
    {
        private readonly IConfigurationStorage _configStore;
        private LightControllerConfig _config;

        public FSLightControllerRepo(IConfigurationStorage configStore)
        {
            _configStore = configStore;
            _config = _configStore.GetConfig<LightControllerConfig>();
        }

        public int Count => _config.Presets.Count;

        public LightTimerPreset GetCurrentPreset()
        {
            if (!string.IsNullOrEmpty(_config.CurrentPresetKey))
            {
                return _config.Presets[_config.CurrentPresetKey];
            }
            else
            {
                throw new InvalidOperationException("CurrentPreset not set before get");
            }
        }

        public void SetCurrentPreset(string presetKey)
        {
            if (_config.CurrentPresetKey != presetKey)
            {
                if (_config.Presets.ContainsKey(presetKey))
                {
                    _config.CurrentPresetKey = presetKey;
                }
                else
                {
                    throw new InvalidOperationException(
                        $"Cannot set current preset. Key:{presetKey} not contains in presets.");
                }
            }
        }

        public List<LightTimerPreset> GetAllPresets()
        {
            return _config.Presets.Values.ToList();
        }

        public LightTimerPreset GetPreset(string key)
        {
            if (_config.Presets.ContainsKey(key))
                return _config.Presets[key];
            return null;
        }

        public void AddPreset(LightTimerPreset preset)
        {
            if (string.IsNullOrEmpty(preset.Key))
                preset.Key = GetValidKey();


            if (!_config.Presets.ContainsKey(preset.Key))
            {
                _config.Presets.Add(preset.Key, preset);
            }
        }

        public void RemovePreset(LightTimerPreset preset)
        {
            if (_config.Presets.ContainsKey(preset.Key))
            {
                _config.Presets.Remove(preset.Key);
            }
        }

        public void UpdatePreset(LightTimerPreset preset)
        {
            throw new NotImplementedException();
        }

        public bool Exist(string key)
        {
            return _config.Presets.ContainsKey(key);
        }

        private string GetValidKey()
        {
            var keyPrefix = "LightPreset";

            int counter = 0;
            for (int i = 0; i < int.MaxValue; i++)
            {
                var key = keyPrefix + i;
                if (!_config.Presets.ContainsKey(key))
                {
                    counter = i;
                    break;
                }
            }

            return keyPrefix + counter;
        }
    }
}