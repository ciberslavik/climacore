using Clima.Core.Devices.Configuration;
using Clima.Core.IO;

namespace Clima.Core.Devices
{
    public class ThyristorConverter:IFrequencyConverter
    {
        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public void SetPower(double power)
        {
            AnalogPin.SetValue(power);
        }

        public double Power { get; }
        
        internal FrequencyConverterConfig Configuration { get; set; }
        internal IAnalogOutput AnalogPin { get; set; }
    }
}