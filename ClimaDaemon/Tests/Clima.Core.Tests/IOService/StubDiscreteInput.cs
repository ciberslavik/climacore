using Clima.Basics;
using Clima.Core.IO;

namespace Clima.Core.Tests.IOService
{
    public class StubDiscreteInput :ObservableObject, IDiscreteInput
    {
        private bool _state;
        public PinType PinType => PinType.Discrete;
        public PinDir Direction => PinDir.Input;
        public string Description { get; set; }
        public string PinName { get; set; }
        public bool IsModified { get; }
        public event DiscretePinStateChangedEventHandler PinStateChanged;

        public bool State
        {
            get => _state;
            private set
            {
                Update(ref _state, value);
                OnPinStateChanged(new DiscretePinStateChangedEventArgs(this, _state));
            }
        }

        internal void SetState(bool state)
        {
            State = state;
        }

        protected virtual void OnPinStateChanged(DiscretePinStateChangedEventArgs ea)
        {
            PinStateChanged?.Invoke(ea);
        }
    }
}