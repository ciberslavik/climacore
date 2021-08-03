using System.Collections.Generic;

namespace Clima.Core.DataModel.GraphModel
{
    public interface IGraphProvider<TGraph>
    {
        TGraph GetCurrentGraph();
        void SetCurrentGraph();
        TGraph GetGraph(string graphName);
        IEnumerable<GraphInfo> GetGraphInfos();

        void AddGraph(string graphName, TGraph graph);
        void RemoveGraph(string graphName);
        void Save();
    }
}