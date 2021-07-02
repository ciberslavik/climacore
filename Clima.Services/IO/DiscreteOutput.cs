using System;

namespace Clima.Services.IO
{
    public interface IDiscreteOutput:IPin
    {
        event DiscretePinStateChangedEventHandler PinStateChanged;
        
        bool State { get; }
        void SetState(bool state, bool queued);
    }
}
