using System;

namespace Clima.Core.IO
{
    public delegate void DiscretePinStateChangedEventHandler(DiscretePinStateChangedEventArgs ea);

    public class DiscretePinStateChangedEventArgs : EventArgs
    {
        private bool _newState;
        private IPin _pin;

        public DiscretePinStateChangedEventArgs(IPin pin, bool newState)
        {
            _pin = pin;
            _newState = newState;
        }

        public bool NewState => _newState;
        public IPin Pin => _pin;
    }
}