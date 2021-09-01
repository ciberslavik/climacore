using System;
using Clima.Core.DataModel;
using Clima.Core.Devices.Configuration;

namespace Clima.Core.Devices
{
    public class AnalogFan : IAnalogFan
    {
        //private FanConfig _config;

        internal AnalogFan()
        {
            //_config = config;
        }


        public void Start()
        {
            FrequencyConverter.SetPower(50);
           
        }

        public void Stop()
        {
            Console.WriteLine($"Fan:{State.Info.Key} stopped.");
        }

        public FanState State { get; } = new FanState();
        
        public double Power { get; set; }

        public IFrequencyConverter FrequencyConverter { get; set; }
        public int CompareTo(IFan? other)
        {
            return State.Info.Priority - other.State.Info.Priority;
        }
    }
}