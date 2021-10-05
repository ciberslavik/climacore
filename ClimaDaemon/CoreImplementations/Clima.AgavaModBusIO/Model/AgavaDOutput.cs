using Clima.Core.IO;


namespace Clima.AgavaModBusIO.Model
{
    public class AgavaDOutput : AgavaPinBase, IDiscreteOutput
    {
        private bool _state;
        internal AgavaDOutput(byte moduleId, int pinNumberInModule)
        {
            _pinNumberInModule = pinNumberInModule;
            PinMask = (ushort)(1 << pinNumberInModule);
            _regAddress = (ushort) (10000 + _pinNumberInModule / 16);
            _moduleId = moduleId;
        }


        public event DiscretePinStateChangedEventHandler PinStateChanged;

        public bool State => _state;

        public void SetState(bool state, bool queued)
        {
            if (_state != state)
            {
                _state = state;
                OnPinStateChanged(new DiscretePinStateChangedEventArgs(this, _state));
            }
        }

        protected virtual void OnPinStateChanged(DiscretePinStateChangedEventArgs ea)
        {
            PinStateChanged?.Invoke(ea);
        }

        internal ushort PinMask { get; }

        public override PinType PinType => PinType.Discrete;
        public override PinDir Direction => PinDir.Output;
    }
}