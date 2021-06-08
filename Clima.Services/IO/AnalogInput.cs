using System;

namespace Clima.Services.IO
{
    public delegate void AInputEventHandler(AnalogInputEventArgs arg);
    public class AnalogInput : PinBase
    {
        protected AnalogInput()
        {
            
        }
        public event AInputEventHandler ValueReady;
        public override PinType PinType => PinType.Analog;
        public override PinDir Direction => PinDir.Input;

        public IAnalogValueConverter ValueConverter { get; set; }
        protected virtual void OnValueReady()
        {
            ValueReady?.Invoke(new AnalogInputEventArgs());
        }
    }
}
