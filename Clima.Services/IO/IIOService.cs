using System;
using System.Collections.Generic;

namespace Clima.Services.IO
{
    public interface IIOService
    {
        void Init();
        void Start();
        void Stop();
        IList<DiscreteInput> DiscreteInputs{get;}
        IList<DiscreteOutput> DiscreteOutputs{get;}
    }
}
