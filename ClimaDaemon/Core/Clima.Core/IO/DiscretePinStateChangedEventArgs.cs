using System;

namespace Clima.Core.IO
{
    public delegate void DiscretePinStateChangedEventHandler(DiscretePinStateChangedEventArgs ea);

    public class DiscretePinStateChangedEventArgs : EventArgs
    {
        private bool _newState;
        private bool _prevState;
        private IPin _pin;

        public DiscretePinStateChangedEventArgs(IPin pin, bool prevState, bool newState)
        {
            _pin = pin;
            _prevState = prevState;
            _newState = newState;
        }

        public bool NewState => _newState;
        public bool PrevState => _prevState;

        public IPin Pin => _pin;
    }
}