using Clima.Services.IO;

namespace Clima.AgavaModBusIO.Model
{
    public class AgavaDInput : AgavaPinBase, IDiscreteInput
    {
        
        private bool _state;

        public AgavaDInput(byte moduleId, int pinNumberInModule)
        {
            _pinNumberInModule = pinNumberInModule;
            _regAddress = (ushort)(10000 + (_pinNumberInModule / 16));
            _moduleId = moduleId;
        }
        
        internal void SetState(bool state)
        {
            if (_state != state)
            {
                bool prevState = _state;
                _state = state;
                var eventargs = new DiscretePinStateChangedEventArgs(this, prevState, _state);
            }
        }
        public event DiscretePinStateChangedEventHandler PinStateChanged;

        public bool State => _state;

        protected virtual void OnPinStateChanged(DiscretePinStateChangedEventArgs ea)
        {
            PinStateChanged?.Invoke(ea);
        }
    }
}
