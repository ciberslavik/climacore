using System;
using Clima.Basics.Services.Communication;
using Clima.Core.Controllers.Light;
using Clima.Core.Controllers.Network.Messages;
using Clima.Core.Controllers.Network.Messages.Light;
using Clima.Core.DataModel.GraphModel;
using Clima.Core.Network.Messages;

namespace Clima.Core.Controllers.Network.Services
{
    public class LightControllerService : INetworkService
    {
        private readonly ILightController _lightController;
        
        public LightControllerService(ILightController lightController)
        {
            _lightController = lightController;
        }

        [ServiceMethod]
        public LightStatusResponse GetLightStatus(DefaultRequest request)
        {
            return new LightStatusResponse()
            {
                CurrentProfileKey = _lightController.CurrentProfile.Key,
                CurrentProfileName = _lightController.CurrentProfile.Name,
                IsAuto = _lightController.IsAuto,
                IsOn = _lightController.IsOn
            };
        }
        [ServiceMethod]
        public DefaultResponse ToManual(DefaultRequest request)
        {
            if(_lightController.IsAuto)
                _lightController.ToManual();

            return new DefaultResponse();
        }

        [ServiceMethod]
        public DefaultResponse ToAuto(DefaultRequest request)
        {
            if(!_lightController.IsAuto)
                _lightController.ToAuto();

            return new DefaultResponse();
        }

        [ServiceMethod]
        public DefaultResponse LightOn(DefaultRequest request)
        {
            _lightController.On();
            return new DefaultResponse();
        }

        [ServiceMethod]
        public DefaultResponse LightOff(DefaultRequest request)
        {
            _lightController.Off();
            return new DefaultResponse();
        }

        [ServiceMethod]
        public LightProfileResponse CreateProfile(CreateLightProfileRequest request)
        {
            return new LightProfileResponse(_lightController.CreateProfile(request.ProfileName));
        }

        [ServiceMethod]
        public LightProfileListResponse GetProfileInfoList(DefaultRequest request)
        {
            var response = new LightProfileListResponse();
            foreach (var profile in _lightController.Profiles.Values)
            {
                response.Profiles.Add(new LightTimerProfileInfo(profile.Key, profile.Name, profile.Description));
            }
            return response;
        }

        [ServiceMethod]
        public LightProfileResponse GetProfile(string key)
        {
            var response = new LightProfileResponse();
            if (_lightController.Profiles.ContainsKey(key))
            {
                response.Profile = _lightController.Profiles[key];
            }

            return response;
        }

        [ServiceMethod]
        public LightProfileResponse GetCurrentProfile(DefaultRequest request)
        {
            return new LightProfileResponse()
            {
                Profile = _lightController.CurrentProfile
            };
        }

        [ServiceMethod]
        public DefaultResponse SetCurrentProfile(string profileKey)
        {
            if (_lightController.Profiles.ContainsKey(profileKey))
            {
                _lightController.SetCurrentProfileKey(profileKey);
                return new DefaultResponse();
            }
            else
            {
                return new DefaultResponse("ProfileNotFound");
            }
        }
        [ServiceMethod]
        public CurrentLightProfileInfoResponse GetCurrentProfileInfo(DefaultRequest request)
        {
            return new CurrentLightProfileInfoResponse()
            {
                ProfileInfo = new ProfileInfo(_lightController.CurrentProfile)
            };
        }
        [ServiceMethod]
        public DefaultResponse UpdateProfile(LightTimerProfile profile)
        {
            _lightController.UpdateProfile(profile);
            return new DefaultResponse();
        }
    }
}