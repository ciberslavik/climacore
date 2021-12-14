using System;
using System.Collections.Generic;
using Clima.Basics.Configuration;
using Clima.Basics.Services;
using Clima.Core.Alarm;
using Clima.Core.Controllers.Configuration;
using Clima.Core.Controllers.Light;
using Clima.Core.IO;

namespace Clima.Core.Controllers
{
    public class LightController : ILightController, IController, IAlarmNotifier
    {
        private readonly IIOService _ioService;
        private ServiceState _serviceState;
        private LightControllerConfig _config;
        private LightTimerProfile _currentProfile;

        private IDiscreteOutput _controlPin;
        private bool _isAlarm;
        public ISystemLogger Log { get; set; }
        public Type ConfigurationType => typeof(LightControllerConfig);
        public LightTimerProfile CurrentProfile => _currentProfile;
        public LightController(IIOService ioService)
        {
            _ioService = ioService;
            _config = LightControllerConfig.CreateDefault();
            _isAlarm = false;
        }

        public void Start()
        {

        }

        public void Stop()
        {

        }
        
        public void ReloadConfig(object cnfig)
        {
            throw new NotImplementedException();
        }

        public void Process(object? context)
        {

        }

        public void Initialize(object config)
        {
            Log.Debug("Initialize Light controller");
            if (config is LightControllerConfig c)
            {
                _config = c;

                if (_config.ControlPinName is null)
                    return;

                if (_ioService.Pins.DiscreteOutputs.ContainsKey(_config.ControlPinName))
                {
                    _controlPin = _ioService.Pins.DiscreteOutputs[_config.ControlPinName];
                }
                else
                {
                    Log.Error($"Light controller control pin \"{_config.ControlPinName}\" not exist");
                }

                if (_config.Profiles.ContainsKey(_config.CurrentProfileKey ?? ""))
                {
                    _currentProfile = _config.Profiles[_config.CurrentProfileKey ?? ""];
                }
                else
                {
                    Log.Error($"Light profile \"{_config.CurrentProfileKey}\" not exist");
                }
            }

            Log.Debug("Light controller initialised");
        }

        public ServiceState ServiceState => _serviceState;

        public void ProcessLight()
        {
            throw new NotImplementedException();
        }

        public void SetCurrentProfileKey(string profileKey)
        {
            if (_currentProfile.Key != profileKey)
            {
                if (_config.Profiles.ContainsKey(profileKey))
                {
                    _currentProfile = _config.Profiles[profileKey];
                }
                else
                {
                    Log.Error($"Light profile \"{profileKey}\" not exist");
                }
            }
        }

        public LightTimerProfile CreateProfile(string profileName)
        {
            var key = GetNewProfileKey();
            var profile = new LightTimerProfile()
            {
                Key = key,
                Name = profileName
            };

            _config.Profiles.Add(key, profile);
            ClimaContext.Current.SaveConfiguration();
            return profile;
        }

        public void UpdateProfile(LightTimerProfile profile)
        {
            throw new NotImplementedException();
        }

        public void RemoveProfile(string profileKey)
        {
            throw new NotImplementedException();
        }

        private string GetNewProfileKey()
        {
            const string prefix = "PROFILE:";
            var newKey = "";
            for (var i = 0; i < int.MaxValue; i++)
            {
                if (!_config.Profiles.ContainsKey(prefix + i))
                {
                    newKey = prefix + i;
                    break;
                }
            }

            return newKey;
        }

        public Dictionary<string, LightTimerProfile> Profiles => _config.Profiles;
        public event EventHandler<AlarmEventArgs> Notify;

        public bool IsAlarm => _isAlarm;

        public bool Reset()
        {
            throw new NotImplementedException();
        }

        protected virtual void OnNotify(AlarmEventArgs ea)
        {
            Notify?.Invoke(this, ea);
        }
    }
}