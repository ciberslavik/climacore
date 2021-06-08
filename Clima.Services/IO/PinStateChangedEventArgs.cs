using System;

namespace Clima.Services.IO
{
    public class PinStateChangedEventArgs : EventArgs
    {
        private bool _pinState;
        public PinStateChangedEventArgs()
        {
        }


        public bool PinState
        {
            get => _pinState;
            set => _pinState = value;
        }
    }
}