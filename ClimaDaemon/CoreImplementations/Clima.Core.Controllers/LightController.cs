using System;
using System.Collections.Generic;
using System.Linq;
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
                {
                    Log.Error("Light control pin not configured");
                    return;
                }

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

        public void ProcessLight(int currentDay)
        {
            if (_config.IsAuto)
            {
                if (CheckTimer(currentDay, DateTime.Now.TimeOfDay))
                {
                    if (_controlPin.State == false)
                    {
                        _controlPin.SetState(true);
                        Log.Debug("Light auto on");
                    }
                }
                else
                {
                    if (_controlPin.State == true)
                    {
                        _controlPin.SetState(false);
                        Log.Debug("Light auto off");
                    }
                }
            }
        }

        private bool CheckTimer(int currentDay, TimeSpan currentTime)
        {
            var profile = _config.Profiles[_config.CurrentProfileKey];

            var day = (from n1 in profile.Days
                where n1.DayNumber < currentDay
                orderby n1.DayNumber descending
                select n1).First();
            
            if (day is not null)
            {
                foreach (var timer in day.Timers)
                {
                    var ontime = timer.OnTime.TimeOfDay;
                    var offtime = timer.OffTime.TimeOfDay;
                    
                    if ((ontime <= currentTime) && (offtime >= currentTime))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public void SetCurrentProfileKey(string profileKey)
        {
            if (_currentProfile.Key != profileKey)
            {
                if (_config.Profiles.ContainsKey(profileKey))
                {
                    _currentProfile = _config.Profiles[profileKey];
                    _config.CurrentProfileKey = _currentProfile.Key;
                    ClimaContext.Current.SaveConfiguration();
                }
                else
                {
                    Log.Error($"Light profile \"{profileKey}\" not exist");
                }
            }
            else
            {
                _currentProfile = _config.Profiles[profileKey];
                _config.CurrentProfileKey = _currentProfile.Key;
                ClimaContext.Current.SaveConfiguration();
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
            if (profile is null)
                throw new ArgumentNullException(nameof(profile));

            if (_config.Profiles.ContainsKey(profile.Key))
            {
                _config.Profiles[profile.Key] = profile;
                ClimaContext.Current.SaveConfiguration();
            }
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

        public bool IsAuto => _config.IsAuto;

        public bool IsOn => _controlPin.State;
        public void On()
        {
            if(_config.IsAuto)
                return;
            if(_controlPin.State == true)
                return;
            
            _controlPin.SetState(true);    
        }

        public void Off()
        {
            if(_config.IsAuto)
                return;
            if(_controlPin.State == false)
                return;
            
            _controlPin.SetState(false);
        }

        public void ToManual()
        {
            if (_config.IsAuto)
            {
                Log.Debug("Light to manual");
                _config.IsAuto = false;
                ClimaContext.Current.SaveConfiguration();
            }
        }

        public void ToAuto()
        {
            if (!_config.IsAuto)
            {
                Log.Debug("Light to auto");
                _config.IsAuto = true;
                ClimaContext.Current.SaveConfiguration();
            }
        }

        public bool Reset()
        {
            return true;
        }

        protected virtual void OnNotify(AlarmEventArgs ea)
        {
            Notify?.Invoke(this, ea);
        }
    }
}