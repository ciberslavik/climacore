using System;

namespace Clima.Services.IO
{
    public class DiscreteOutput:PinBase
    {
        public event PinStateChangedEventHandler PinStateChanged;
        private bool _pinState;
        private bool _pinPrevState;
        public override PinType PinType
        {
            get
            {
                return PinType.Discrete;
            }
        }

        public override PinDir Direction
        {
            get
            {
                return PinDir.Output;
            }
        }

        public bool State
        {
            get => _pinState;
            set
            {
                if (_pinState != value)
                {
                    _pinState = value;
                    OnPinStateChanged(new PinStateChangedEventArgs());
                }
            }
        }

        protected virtual void OnPinStateChanged(PinStateChangedEventArgs args)
        {
            PinStateChanged?.Invoke(this, args);
        }
    }
}
