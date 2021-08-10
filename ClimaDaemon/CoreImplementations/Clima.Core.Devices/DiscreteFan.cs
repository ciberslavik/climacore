using System;

namespace Clima.Core.Devices
{
    public class DiscreteFan : IDiscreteFan
    {
        public DiscreteFan()
        {
        }


        public void Start()
        {
            if (!FanRelay.RelayIsOn)
                FanRelay.On();
            
            Console.WriteLine($"Fan:{State.FanId} started.");
        }

        public void Stop()
        {
            FanRelay.Off();
            Console.WriteLine($"Fan:{State.FanId} stopped.");
        }

        public FanState State { get; }


        public IRelay FanRelay { get; set; }
        public int CompareTo(IFan? other)
        {
            return State.Priority - other.State.Priority;
        }
    }
}