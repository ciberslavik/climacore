using System.Collections.Generic;
using Clima.Core.DataModel.GraphModel;
using Clima.FSGrapRepository.Configuration;

namespace Clima.FSGrapRepository
{
    public abstract class GraphProviderBase<TGraph, TPoint, TConfig> : IGraphProvider<TGraph> 
        where TGraph:GraphBase<TPoint>
        where TPoint:GraphPointBase
        where TConfig:IGraphPointConfig<TPoint>
    {
        protected TGraph _currentGraph;
        protected TConfig _config;

        protected GraphProviderBase(TConfig config)
        {
            _config = config;
        }
        public TGraph GetCurrentGraph()
        {
            if (!(_currentGraph is null))
                return _currentGraph;

            return null;
        }

        public void SetCurrentGraph()
        {
            throw new System.NotImplementedException();
        }

        public TGraph GetGraph(string graphName)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<GraphInfo> GetGraphInfos()
        {
            throw new System.NotImplementedException();
        }

        public void AddGraph(string graphName, TGraph graph)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveGraph(string graphName)
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }
    }
}