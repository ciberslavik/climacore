using Clima.DataModel.Process.ShedulerGraphs;

namespace Clima.DataModel.Process
{
    public class ShedulerContext
    {
        private VentelationGraph _ventelationGraph;

        public ShedulerContext()
        {
            _ventelationGraph = new VentelationGraph();
        }
        public int CurrentDay { get; set; }

        public VentelationGraph VentelationGraph => _ventelationGraph;

        public void SetCurrentVentelationGraph(VentelationGraph graph)
        {
            _ventelationGraph = graph;
        }
    }
}