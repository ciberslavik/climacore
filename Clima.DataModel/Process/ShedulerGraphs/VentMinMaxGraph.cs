using System.Collections.Generic;

namespace Clima.DataModel.Process.ShedulerGraphs
{
    public class VentMinMaxGraph : GraphPressetBase
    {
        public VentMinMaxGraph()
        {
        }

        public List<VentMinMaxPoint> Points { get; set; }
    }
}