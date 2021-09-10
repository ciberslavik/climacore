using System;
using Clima.Core.DataModel;

namespace Clima.Core.Scheduler.Network.Messages
{
    public class ProductionStateResponse
    {
        public ProductionStateResponse()
        {
            
        }
       public ProductionState State { get; set; } = new ProductionState();
    }
}