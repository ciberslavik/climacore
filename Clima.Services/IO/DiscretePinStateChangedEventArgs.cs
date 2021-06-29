using System;

namespace Clima.Services.IO
{
    public delegate void DiscretePinStateChangedEventHandler(DiscretePinStateChangedEventArgs ea);
    public class DiscretePinStateChangedEventArgs : EventArgs
    {
        private bool _newState;
        private bool _prevState;
        private PinBase _pin;
        public DiscretePinStateChangedEventArgs(PinBase pin, bool prevState, bool newState)
        {
            _pin = pin;
            _prevState = prevState;
            _newState = newState;
        }
        public bool NewState => _newState;
        public bool PrevState => _prevState;

        public PinBase Pin => _pin;
    }
}