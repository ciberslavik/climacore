using System;
using System.Collections.Generic;

namespace Clima.Services.IO
{
    public interface IIOService
    {
        void Init();
        void Start();
        void Stop();
        IDictionary<string, DiscreteInput> DiscreteInputs{get;}
        IDictionary<string, DiscreteOutput> DiscreteOutputs{get;}
    }
}
