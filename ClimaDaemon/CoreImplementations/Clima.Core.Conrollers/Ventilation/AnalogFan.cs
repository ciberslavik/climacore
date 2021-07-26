using Clima.Core.Conrollers.Ventilation.Ventilation.Configuration;
using Clima.Core.Controllers.Ventilation;
using Clima.Core.Devices;

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

        public int FanId
        {
            get => _config.FanId;
            set => _config.FanId = value;
        }
        public string FanName 
        { 
            get => _config.FanName;
            set => _config.FanName = value;
        }

        public bool Disabled
        {
            get => _config.Disabled;
            set => _config.Disabled = value;
        }

        public bool Hermetise { get=>_config.Hermetise; set=>_config.Hermetise=value; }
        public int Performance { get=>_config.Performance; set=>_config.Performance=value; }
        public int FansCount { get=>_config.FansCount; set=>_config.FansCount=value; }
        public int Priority { get=>_config.FanPriority; set=>_config.FanPriority=value; }
        public double StartValue { get=>_config.StartPower; set=>_config.StartPower=value; }
        public double StopValue { get=>_config.StopPower; set=>_config.StopPower=value; }
        
        public double Power { get; set; }
        
        public IFrequencyConverter FrequencyConverter { get; set; }
    }
}