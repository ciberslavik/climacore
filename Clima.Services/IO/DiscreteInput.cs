using System;

namespace Clima.Services.IO
{
    
    public class DiscreteInput:PinBase
    {
        private bool _prevPinState;
        private bool _state;
        public event DiscretePinStateChangedEventHandler PinStateChanged;
        public override PinType PinType => PinType.Discrete;

        public override PinDir Direction => PinDir.Input;

        protected virtual void OnPinStateChanged(DiscretePinStateChangedEventArgs args)
        {
            PinStateChanged?.Invoke(args);
        }

        public bool State
        {
            get => _state;
            set
            {
                if (_state != value)
                {
                    _prevPinState = _state;
                    _state = value;
                    OnPinStateChanged(new DiscretePinStateChangedEventArgs(this, _prevPinState, _state));
                }
            }
        }
    }
}
