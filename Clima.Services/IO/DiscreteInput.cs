using System;

namespace Clima.Services.IO
{
    public delegate void PinStateChangedEventHandler(PinStateChangedEventArgs args);
    public class DiscreteInput:PinBase
    {
        public event PinStateChangedEventHandler PinStateChanged;
        public override PinType PinType => PinType.Discrete;

        public override PinDir Direction => PinDir.Input;

        protected virtual void OnPinStateChanged(PinStateChangedEventArgs args)
        {
            PinStateChanged?.Invoke(args);
        }
    }
}
