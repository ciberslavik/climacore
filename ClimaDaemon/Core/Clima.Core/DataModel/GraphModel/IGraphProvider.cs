using System.Collections.Generic;

namespace Clima.Core.DataModel.GraphModel
{
    public interface IGraphProvider<TGraph, TPoint>
        where TPoint : GraphPointBase
        where TGraph : GraphBase<TPoint>
    {
        TGraph GetCurrentGraph();
        void SetCurrentGraph(TGraph graph);
        TGraph GetGraph(string graphName);
        IList<GraphInfo> GetGraphInfos();

        void AddGraph(TGraph graph);
        void RemoveGraph(string key);

        TGraph CreateGraph(string key);
        string GetValidKey();
        bool ContainsKey(string key);
    }
}