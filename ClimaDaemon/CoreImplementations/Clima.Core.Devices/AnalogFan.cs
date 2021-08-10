using System;
using Clima.Core.Devices.Configuration;

namespace Clima.Core.Devices
{
    public class AnalogFan : IAnalogFan
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
            Console.WriteLine($"Fan:{State.FanId} stopped.");
        }

        public FanState State => _config.CreateFanState();


        public double Power { get; set; }

        public IFrequencyConverter FrequencyConverter { get; set; }
        public int CompareTo(IFan? other)
        {
            return State.Priority - other.State.Priority;
        }
    }
}