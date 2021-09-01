using Clima.Core.IO;

namespace Clima.Core.Tests.IOService
{
    public class StubDiscreteInput : IDiscreteInput
    {
        public PinType PinType => PinType.Discrete;
        public PinDir Direction => PinDir.Input;
        public string Description { get; set; }
        public string PinName { get; set; }
        public bool IsModified { get; }
        public event DiscretePinStateChangedEventHandler PinStateChanged;
        public bool State { get; private set; }

        internal void SetState(bool state)
        {
            State = state;
        }
    }
}