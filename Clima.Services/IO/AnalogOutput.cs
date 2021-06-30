using System;

namespace Clima.Services.IO
{
    public class AnalogOutput : PinBase
    {
        public event AnalogPinValueChangedEventHandler ValueChanged;
        private double _rawValue;
        private double _value;
        private double _oldValue;
        public override PinType PinType
        {
            get
            {
                return PinType.Analog;
            }
        }

        public override PinDir Direction
        {
            get
            {
                return PinDir.Output;
            }
        }
        public virtual double Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _oldValue = _value;
                    _value = value;
                    OnValueChanged(_oldValue,_value);
                }
            }
            
        }

        protected virtual void OnValueChanged(double prevValue, double newValue)
        {
            AnalogPinValueChangedEventArgs ea = 
                new AnalogPinValueChangedEventArgs(this, prevValue, newValue);
            
            ValueChanged?.Invoke(ea);
        }
    }
}
