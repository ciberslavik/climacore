using Clima.Core.IO;

namespace Clima.Core.Tests.IOService
{
    public class StubDiscreteOutput : IDiscreteOutput
    {
        public PinType PinType => PinType.Discrete;
        public PinDir Direction => PinDir.Output;
        public string Description { get; set; }
        public string PinName { get; set; }
        public bool IsModified { get; }
        public event DiscretePinStateChangedEventHandler PinStateChanged;
        public bool State { get; }

        public void SetState(bool state, bool queued = true)
        {
            throw new System.NotImplementedException();
        }
    }
}