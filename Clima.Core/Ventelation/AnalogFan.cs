using Clima.Services.Devices;

namespace Clima.Core.Ventelation
{
    public class AnalogFan
    {
        private readonly FrequencyConverter _fc;

        public AnalogFan(FrequencyConverter fc)
        {
            _fc = fc;
        }

        public void Start()
        {
            
        }
        public void Stop()
        {
            
        }
        public bool IsRunning { get; set; }
    }
}