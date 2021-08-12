using Clima.Core.IO;


namespace Clima.AgavaModBusIO.Model
{
    public class AgavaDOutput : AgavaPinBase, IDiscreteOutput
    {
        private bool _state;
        private bool _prevState;

        internal AgavaDOutput(byte moduleId, int pinNumberInModule)
        {
            _pinNumberInModule = pinNumberInModule;
            _regAddress = (ushort) (10000 + _pinNumberInModule / 16);
            _moduleId = moduleId;
        }


        public event DiscretePinStateChangedEventHandler PinStateChanged;

        public bool State => _state;

        public void SetState(bool state, bool queued)
        {
            if (_state != state)
            {
                _prevState = _state;
                _state = state;
                OnPinStateChanged(new DiscretePinStateChangedEventArgs(this, _prevState, _state));
            }
        }

        protected virtual void OnPinStateChanged(DiscretePinStateChangedEventArgs ea)
        {
            PinStateChanged?.Invoke(ea);
        }

        public override PinType PinType => PinType.Discrete;
        public override PinDir Direction => PinDir.Output;
    }
}