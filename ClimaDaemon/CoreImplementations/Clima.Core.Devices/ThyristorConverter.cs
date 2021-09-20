using Clima.Core.Devices.Configuration;
using Clima.Core.IO;

namespace Clima.Core.Devices
{
    public class ThyristorConverter : IFrequencyConverter
    {
        private bool _isRunning;
        public void Start()
        {
            if (!_isRunning)
            {
                _isRunning = true;
            }
        }

        public void Stop()
        {
            if (_isRunning)
            {
                _isRunning = false;
            }
        }

        public void SetPower(float power)
        {
            if (_isRunning)
            {
                AnalogPin.SetValue(power);
            }
        }

        public float Power => AnalogPin.Value;

        internal FrequencyConverterConfig Configuration { get; set; }
        internal IAnalogOutput AnalogPin { get; set; }
    }
}