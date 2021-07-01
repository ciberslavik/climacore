using System;
using System.Timers;
using Clima.Services.Devices;
using NSubstitute;

namespace Clima.Services.Tests.Devices
{
    public class FakeTimer:ITimer
    {
        private double _interval;

        public void Start()
        {
            Console.WriteLine("Timer started");
        }

        public void Stop()
        {
            Console.WriteLine("Timer stoped");
        }

        public double Interval
        {
            get => _interval;
            set => _interval = value;
        }

        public void InvokeElapsed()
        {
            Elapsed?.Invoke(this);
        }
        public event TimerElapsedEventHandler Elapsed;
    }
}