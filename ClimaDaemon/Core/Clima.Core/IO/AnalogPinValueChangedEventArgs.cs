using System;

namespace Clima.Core.IO
{
    public delegate void AnalogPinValueChangedEventHandler(AnalogPinValueChangedEventArgs ea);

    public class AnalogPinValueChangedEventArgs : EventArgs
    {
        private IPin _pin;
        private float _prevValue;
        private float _newValue;

        public AnalogPinValueChangedEventArgs(IPin pin, float prevValue, float newValue)
        {
            _pin = pin;
            _prevValue = prevValue;
            _newValue = newValue;
        }


        public IPin Pin => _pin;

        public float PrevValue => _prevValue;

        public float NewValue => _newValue;
    }
}