using System;
using Clima.Core.DataModel;

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
            
            Console.WriteLine($"Fan:{Info.Key} started.");
        }

        public void Stop()
        {
            FanRelay.Off();
            Console.WriteLine($"Fan:{Info.Key} stopped.");
        }

        public FanState State { get; }

        public FanInfo Info { get; }
        public IRelay FanRelay { get; set; }
        public int CompareTo(IFan? other)
        {
            return State.Priority - other.State.Priority;
        }
    }
}