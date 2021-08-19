using Clima.Basics.Services.Communication;
using Clima.Core.Controllers.Light;
using Clima.Core.Controllers.Network.Messages;
using Clima.Core.Network.Messages;

namespace Clima.Core.Controllers.Network.Services
{
    public class LightControllerService : INetworkService
    {
        private readonly ILightControllerDataRepo _dataRepo;

        public LightControllerService(ILightControllerDataRepo dataRepo)
        {
            _dataRepo = dataRepo;
        }

        [ServiceMethod]
        public DefaultResponse SetManual(DefaultRequest request)
        {
            return new DefaultResponse();
        }

        [ServiceMethod]
        public DefaultResponse SetAuto(DefaultRequest request)
        {
            return new DefaultResponse();
        }

        [ServiceMethod]
        public DefaultResponse ManualOn(DefaultRequest request)
        {
            return new DefaultResponse();
        }

        [ServiceMethod]
        public DefaultResponse ManualOff(DefaultRequest request)
        {
            return new DefaultResponse();
        }

        [ServiceMethod]
        public DefaultResponse CreatePreset(CreateLightPresetRequest request)
        {
            return new DefaultResponse();
        }
    }
}