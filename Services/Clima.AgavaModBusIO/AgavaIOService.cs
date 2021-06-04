using System.Collections.Generic;
using System;
using Clima.Services.IO;
using Clima.AgavaModBusIO.Model;

namespace Clima.AgavaModBusIO
{
    public class AgavaIOService:IIOService
    {
        Dictionary<int, AgavaIOModule> _modules;
        Dictionary<string, DiscreteOutput> _dOutputs;

        public IList<DiscreteInput> DiscreteInputs => throw new NotImplementedException();

        public IList<DiscreteOutput> DiscreteOutputs => throw new NotImplementedException();

        public void Init()
        {

        }
        public void Start()
        {

        }
        public void Stop()
        {

        }
        
    }
}
