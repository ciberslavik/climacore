using System.Collections.Generic;
using System;
using Clima.Services.IO;
namespace Clima.AgavaModBusIO
{
    public class AgavaIOService:IIOSystem
    {
        Dictionary<string, DiscreteInput> _dInputs;
        Dictionary<string, DiscreteOutput> _dOutputs;
        public void Init()
        {

        }
        public void Start()
        {

        }
        public void Stop()
        {

        }
        public IList<DiscreteInput> DiscreteInputs
        {
            get
            {
                return new List<DiscreteInput>(_dInputs.Values);
            }
        }
        public IList<DiscreteOutput> DiscreteOutputs
        {
            get
            {return new List<DiscreteOutput>(_dOutputs.Values);}
        }
    }
}
