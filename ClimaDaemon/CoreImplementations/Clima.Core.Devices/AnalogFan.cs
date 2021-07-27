using Clima.Core.Controllers.Ventilation;
using Clima.Core.Devices;
using Clima.Core.Devices.Configuration;

namespace Clima.Core.Conrollers.Ventilation.Ventilation
{
    public class AnalogFan:IAnalogFan
    {
        private FanConfig _config;
        internal AnalogFan(FanConfig config)
        {
            _config = config;
        }


        public void Start()
        {
            FrequencyConverter.SetPower(50);
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public FanState State => _config.CreateFanState();


        public double Power { get; set; }
        
        public IFrequencyConverter FrequencyConverter { get; set; }

        
    }
}