using System;
using System.IO;
using System.Timers;
using Clima.Services.IO;

namespace FakeIOService
{
    public class FakeControlledRelay
    {
        private readonly FakeIO _io;
        private Timer _onTimer;
        private Timer _offTimer;
        private int _relayNumber = 0;
        public FakeControlledRelay(FakeIO io, int relayNumber)
        {
            _io = io;
            _relayNumber = relayNumber;
            string outName = $"DO:{relayNumber + 2}";
            string inName = $"DI:{relayNumber + 2}";
            _out = _io.DiscreteOutputs[outName];
            _out.PinStateChanged+= OutOnPinStateChanged;
            
            _input = _io.DiscreteInputs[inName];
            
            _onTimer = new Timer(300);
            _onTimer.Elapsed+= OnTimerOnElapsed;
            
            _offTimer = new Timer(2000);
            _offTimer.Elapsed+= OffTimerOnElapsed;
            
        }

        private void OutOnPinStateChanged(DiscretePinStateChangedEventArgs args)
        {
            _onTimer.Start();
            Console.WriteLine($"Relay:{_relayNumber} on");
        }

        private void OffTimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            _input.State = false;
            Console.WriteLine($"Relay:{_relayNumber} alarm emulated");
        }

        private void OnTimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine($"Relay:{_relayNumber} on complete");
            _input.State = true;
            _offTimer.Start();
        }

        private DiscreteOutput _out;
        private DiscreteInput _input;

    }
}