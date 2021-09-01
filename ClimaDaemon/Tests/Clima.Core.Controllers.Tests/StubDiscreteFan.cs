using System;
using Clima.Core.DataModel;
using Clima.Core.Devices;

namespace Clima.Core.Controllers.Tests
{
    public class StubDiscreteFan:IDiscreteFan
    {
        public StubDiscreteFan()
        {
            State = new FanState();
        }


        public int CompareTo(IFan? other)
        {
            return State.Info.Priority - other.State.Info.Priority;
        }

        public void Start()
        {
            Console.WriteLine($"Fan:{State.Info.Key} started.");
        }

        public void Stop()
        {
            Console.WriteLine($"Fan:{State.Info.Key} stopped.");
        }
        public FanState State { get; set; }
        public IRelay FanRelay { get; set; }
    }
}