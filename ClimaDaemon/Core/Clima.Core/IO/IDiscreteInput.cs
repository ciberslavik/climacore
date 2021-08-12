namespace Clima.Core.IO
{
    public interface IDiscreteInput : IPin
    {
        event DiscretePinStateChangedEventHandler PinStateChanged;

        public bool State { get; }
    }
}