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
        internal StubDiscreteInput MonitorPin { get; set; }
        public void SetState(bool state, bool queued = true)
        {
            ClimaContext.Current.Logger.Error($"Pin:{PinName} to {state}");
            if (MonitorPin is not null)
            {
                MonitorPin.SetState(state);
            }
        }
    }
}