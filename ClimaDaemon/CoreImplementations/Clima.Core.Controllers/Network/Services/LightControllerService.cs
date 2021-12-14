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
        public DefaultResponse SetManual(DefaultRequest request)
        {
            throw new NotImplementedException();
        }

        [ServiceMethod]
        public DefaultResponse SetAuto(DefaultRequest request)
        {
            throw new NotImplementedException();
        }

        [ServiceMethod]
        public DefaultResponse ManualOn(DefaultRequest request)
        {

            throw new NotImplementedException();
        }

        [ServiceMethod]
        public DefaultResponse ManualOff(DefaultRequest request)
        {
            
            throw new NotImplementedException();
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

            return new DefaultResponse();
        }
    }
}