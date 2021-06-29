using System;

namespace Clima.Services.IO
{
    public class DiscreteOutput:PinBase
    {
        public event DiscretePinStateChangedEventHandler PinStateChanged;
        private bool _pinState;
        private bool _pinPrevState;
        private int _pinIndex;
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
                    OnPinStateChanged(new DiscretePinStateChangedEventArgs(this, _pinState, _pinState));
                }
            }
        }

        protected virtual void OnPinStateChanged(DiscretePinStateChangedEventArgs args)
        {
            PinStateChanged?.Invoke(args);
        }
    }
}
