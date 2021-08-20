using System.Threading;
using Clima.Basics.Configuration;
using Clima.Core.Controllers;
using Clima.Core.Controllers.Light;
using Clima.Core.Controllers.Ventilation;
using Clima.Core.Devices;
using Clima.Core.IO;

namespace Clima.Core.Conrollers.Ventilation
{
    public class ControllerFactory:IControllerFactory
    {
        private readonly ILightControllerDataRepo _lightRepo;
        private readonly IIOService _ioService;
        private readonly IDeviceProvider _deviceProvider;
        private IVentilationController _ventController = null;
        
        public ControllerFactory(ILightControllerDataRepo lightRepo,
            IIOService ioService, IDeviceProvider deviceProvider)
        {
            _lightRepo = lightRepo;
            _ioService = ioService;
            _deviceProvider = deviceProvider;
        }


        public ILightController GetLightController()
        {
            var lightController = new LightController();
            lightController.LightRelay = _deviceProvider.GetRelay("REL:10");
            lightController.Preset = _lightRepo.GetCurrentPreset();
            
            return lightController;
        }

        public IVentilationController GetVentilationController()
        {
            if (_ventController is null)
            {
                
            }

            return _ventController;
        }
    }
}