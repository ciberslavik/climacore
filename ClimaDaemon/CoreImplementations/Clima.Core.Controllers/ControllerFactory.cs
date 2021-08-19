using System.Threading;
using Clima.Basics.Configuration;
using Clima.Core.Controllers;
using Clima.Core.Controllers.Light;
using Clima.Core.Devices;
using Clima.Core.IO;

namespace Clima.Core.Conrollers.Ventilation
{
    public class ControllerFactory:IControllerFactory
    {
        private readonly ILightControllerDataRepo _lightRepo;
        private readonly IIOService _ioService;
        private readonly IDeviceProvider _deviceProvider;
        
        public ControllerFactory(ILightControllerDataRepo lightRepo,
            IIOService ioService, IDeviceProvider deviceProvider)
        {
            _lightRepo = lightRepo;
            _ioService = ioService;
            _deviceProvider = deviceProvider;
        }


        public ILightController CreateLightController()
        {
            var lightController = new LightController();
            lightController.LightRelay = _deviceProvider.GetRelay("REL:10");
            lightController.Preset = _lightRepo.GetCurrentPreset();
            
            return lightController;
        }
    }
}