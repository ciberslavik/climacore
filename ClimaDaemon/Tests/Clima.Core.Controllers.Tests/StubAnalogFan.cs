using System;
using Clima.Core.Devices;

namespace Clima.Core.Controllers.Tests
{
    public class StubAnalogFan:IAnalogFan
    {
        public StubAnalogFan()
        {
            State = new FanState();
        }


        public int CompareTo(IFan? other)
        {
            return State.Priority - other.State.Priority;
        }

        public void Start()
        {
            Console.WriteLine($"Fan:{State.FanId} started.");
        }

        public void Stop()
        {
            Console.WriteLine($"Fan:{State.FanId} stopped.");
        }

        public FanState State { get; set; }
        public double Power { get; set; }
        public IFrequencyConverter FrequencyConverter { get; set; }
    }
}