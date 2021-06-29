using System;

namespace Clima.Services.IO
{
    public delegate void AnalogPinValueChangedEventHandler(AnalogPinValueChangedEventArgs ea);
    public class AnalogPinValueChangedEventArgs:EventArgs
    {
        private PinBase _pin;
        private double _prevValue;
        private double _newValue;

        public AnalogPinValueChangedEventArgs(PinBase pin, double prevValue, double newValue)
        {
            _pin = pin;
            _prevValue = prevValue;
            _newValue = newValue;
        }


        public PinBase Pin => _pin;

        public double PrevValue => _prevValue;

        public double NewValue => _newValue;
    }
}