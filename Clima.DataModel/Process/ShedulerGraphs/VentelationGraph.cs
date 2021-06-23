using System;
using System.Collections.Generic;

namespace Clima.DataModel.Process.ShedulerGraphs
{
    public class VentelationGraph: GraphPressetBase
    {

        public VentelationGraph()
        {

        }

        public IList<VentelationPoint> Points {get; set; }
    }
}