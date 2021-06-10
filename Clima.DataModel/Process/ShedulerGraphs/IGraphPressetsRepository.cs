using System.Collections.Generic;

namespace Clima.DataModel.Process.ShedulerGraphs
{
    public interface IGraphPressetsRepository
    {
        IList<VentelationGraph> GetVentelationGraphs();
        VentelationGraph GetVentelationGraph();
    }
}