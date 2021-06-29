using System;

namespace Clima.Services.IO
{
    public class AnalogOutput : PinBase
    {
        public event AnalogPinValueChangedEventHandler ValueChanged;
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
        public virtual double Value { get; set; }

        protected virtual void OnValueChanged(double prevValue, double newValue)
        {
            AnalogPinValueChangedEventArgs ea = 
                new AnalogPinValueChangedEventArgs(this, prevValue, newValue);
            
            ValueChanged?.Invoke(ea);
        }
    }
}
