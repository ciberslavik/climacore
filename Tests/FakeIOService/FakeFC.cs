using System;
using Clima.Services.IO;

namespace FakeIOService
{
    public class FakeFC
    {
        private readonly FakeIO _io;
        private readonly int _fcNumber;
        private DiscreteOutput _enPin;
        private DiscreteInput _alarmPin;
        private AnalogOutput _analogPin;
        
        public FakeFC(FakeIO io, int fcNumber)
        {
            _io = io;
            _fcNumber = fcNumber;
            string enPinName = $"DO:{fcNumber}";
            string alarmPinName = $"DI:{fcNumber}";
            string analogPinName = $"AO:{fcNumber}";

            _enPin = io.DiscreteOutputs[enPinName];
            _enPin.PinStateChanged += EnPinOnPinStateChanged;
            
            _alarmPin = io.DiscreteInputs[alarmPinName];
            _alarmPin.PinStateChanged += AlarmPinOnPinStateChanged;
            
            _analogPin = io.AnalogOutputs[analogPinName];
        }

        private void AlarmPinOnPinStateChanged(DiscretePinStateChangedEventArgs args)
        {
            Console.WriteLine($"FC:{_fcNumber} alarm emulated");
        }

        private void EnPinOnPinStateChanged(DiscretePinStateChangedEventArgs args)
        {
            Console.WriteLine($"FC:{_fcNumber} enabled");
        }
    }
}