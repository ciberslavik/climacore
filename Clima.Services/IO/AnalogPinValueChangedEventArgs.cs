using System;

namespace Clima.Services.IO
{
    public delegate void AnalogPinValueChangedEventHandler(AnalogPinValueChangedEventArgs ea);
    public class AnalogPinValueChangedEventArgs:EventArgs
    {
        private IPin _pin;
        private double _prevValue;
        private double _newValue;

        public AnalogPinValueChangedEventArgs(IPin pin, double prevValue, double newValue)
        {
            _pin = pin;
            _prevValue = prevValue;
            _newValue = newValue;
        }


        public IPin Pin => _pin;

        public double PrevValue => _prevValue;

        public double NewValue => _newValue;
    }
}