using System;
using System.Threading;
using Clima.Core.IO;

namespace Clima.Core.Tests.IOService
{
    public class StubAnalogInput : IAnalogInput
    {
        private Random _random;
        private double _range;
        private float _value;
        private Timer timer;
        public StubAnalogInput()
        {
            _random = new Random();
            _range = 10.0;
            timer = new Timer(Timeout, this, 1000, 1000);
        }

        private void Timeout(object? state)
        {
            if (PinName is "AI:1:0")
            {
                double sample = _random.NextDouble();
                double scaled = (sample * 25) + 17.5;
                
                OnValueChanged(new AnalogPinValueChangedEventArgs(
                        this,_value,(float)scaled));
                _value = (float)scaled;
            }
        }

        public PinType PinType => PinType.Analog;
        public PinDir Direction => PinDir.Input;

        public string Description { get; set; }
        public string PinName { get; set; }
        public bool IsModified { get; }
        public event AnalogPinValueChangedEventHandler ValueChanged;
        public IAnalogValueConverter ValueConverter { get; set; }
        public float Value 
        {
            get
            {
                if (PinName is "AI:1:3" or "AI:1:0")
                {
                    double sample = _random.NextDouble();
                    double scaled = (sample * _range) + 15.0;
                    return (float) scaled;
                }
                else
                {
                    return _value;
                }
            }
            set
            {
                if (PinName != "AI:1:3")
                    _value = value;
            }
        }
        public double RawValue { get; }

        protected virtual void OnValueChanged(AnalogPinValueChangedEventArgs ea)
        {
            ValueChanged?.Invoke(ea);
        }
    }
}