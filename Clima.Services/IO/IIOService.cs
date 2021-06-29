using System;
using System.Collections.Generic;

namespace Clima.Services.IO
{
    public interface IIOService
    {
        void Init();
        void Start();
        void Stop();
        bool IsInit { get; }
        bool IsRunning { get; }
        
        IOPinCollection Pins { get; }
    }
}
