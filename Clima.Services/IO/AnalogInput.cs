using System;

namespace Clima.Services.IO
{
    public class AnalogInput : PinBase
    {
        public AnalogInput()
        {
            
        }

        public event AnalogPinValueChangedEventHandler ValueChanged;
        public override PinType PinType => PinType.Analog;
        public override PinDir Direction => PinDir.Input;

        public IAnalogValueConverter ValueConverter { get; set; }

        protected virtual void OnValueChanged(AnalogPinValueChangedEventArgs ea)
        {
            ValueChanged?.Invoke(ea);
        }
    }
}
