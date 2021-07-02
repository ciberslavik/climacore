using System;

namespace Clima.Services.IO
{
    
    public interface IDiscreteInput:IPin
    {
        event DiscretePinStateChangedEventHandler PinStateChanged;
        
        public bool State { get; }
    }
}
