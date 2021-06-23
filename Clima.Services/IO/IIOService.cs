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
        
        IDictionary<string, DiscreteInput> DiscreteInputs{get;}
        IDictionary<string, DiscreteOutput> DiscreteOutputs{get;}
        IDictionary<string, AnalogOutput> AnalogOutputs { get; }
        IDictionary<string, AnalogInput> AnalogInputs { get; }
    }
}
