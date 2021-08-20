using Clima.Basics.Services.Communication;
using Clima.Core.Controllers.Light;
using Clima.Core.Controllers.Network.Messages;
using Clima.Core.Network.Messages;

namespace Clima.Core.Controllers.Network.Services
{
    public class LightControllerService : INetworkService
    {
        private readonly ILightControllerDataRepo _dataRepo;
        private readonly ILightController _controller;


        public LightControllerService(IControllerFactory controllerFactory, ILightControllerDataRepo dataRepo)
        {
            _dataRepo = dataRepo;
            _controller = controllerFactory.GetLightController();
        }

        [ServiceMethod]
        public DefaultResponse SetManual(DefaultRequest request)
        {
            _controller.IsManual = true;
            return new DefaultResponse();
        }

        [ServiceMethod]
        public DefaultResponse SetAuto(DefaultRequest request)
        {
            _controller.IsManual = false;
            return new DefaultResponse();
        }

        [ServiceMethod]
        public DefaultResponse ManualOn(DefaultRequest request)
        {
            if(_controller.IsManual)
                _controller.ManualOn();
            
            return new DefaultResponse();
        }

        [ServiceMethod]
        public DefaultResponse ManualOff(DefaultRequest request)
        {
            if(_controller.IsManual)
                _controller.ManualOff();
            
            return new DefaultResponse();
        }

        [ServiceMethod]
        public DefaultResponse CreatePreset(CreateLightPresetRequest request)
        {
            return new DefaultResponse();
        }
    }
}