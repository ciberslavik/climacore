using System;

namespace Clima.Services.IO
{
    public delegate void PinStateChangedEventHandler(object sender, PinStateChangedEventArgs args);
    public class DiscreteInput:PinBase
    {
        private bool _prevPinState;
        private bool _state;
        public event PinStateChangedEventHandler PinStateChanged;
        public override PinType PinType => PinType.Discrete;

        public override PinDir Direction => PinDir.Input;

        protected virtual void OnPinStateChanged(PinStateChangedEventArgs args)
        {
            PinStateChanged?.Invoke(this, args);
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
                    OnPinStateChanged(new PinStateChangedEventArgs());
                }
            }
        }
    }
}
