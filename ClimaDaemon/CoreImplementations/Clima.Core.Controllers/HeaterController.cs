using Clima.Basics.Configuration;
using Clima.Core.Controllers.Configuration;
using Clima.Core.Controllers.Heater;
using Clima.Core.Devices;

namespace Clima.Core.Conrollers.Ventilation
{
    public class HeaterController:IHeaterController
    {
        private readonly IDeviceProvider _deviceProvider;
        private VentilationControllerConfig _config;
        public HeaterController(IDeviceProvider deviceProvider)
        {
            _deviceProvider = deviceProvider;
        }

        public void Init(object config)
        {
            if(config is VentilationControllerConfig cfg)
            {
                _config = cfg;
            }
        }
        public HeaterState State1 { get; private set; }
        public HeaterState State2 { get; private set; }
        public void SetHeater1State(HeaterState newState)
        {
            if(State1 == newState)
                return;
            if (newState.IsManual)
            {
                if (newState.IsRunning)
                {
                    
                }
            }
        }

        public void SetHeater2State(HeaterState newState)
        {
            throw new System.NotImplementedException();
        }

        public void Process(float setpoint)
        {
            throw new System.NotImplementedException();
        }
    }
}