using System;
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
        private readonly IIOService _ioService;
        private readonly IDeviceProvider _deviceProvider;
        
        public ControllerFactory(IIOService ioService, IDeviceProvider deviceProvider)
        {
            _ioService = ioService;
            _deviceProvider = deviceProvider;
        }


        public ILightController GetLightController()
        {
            throw new NotImplementedException();
        }

        public IVentilationController GetVentilationController()
        {
            throw new NotImplementedException();
        }
    }
}