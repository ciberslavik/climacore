using System;
using System.Collections.Generic;
using Clima.Core.DataModel.GraphModel;
using Clima.Core.Exceptions;
using Clima.FSGrapRepository.Configuration;

namespace Clima.FSGrapRepository
{
    public abstract class GraphProviderBase<TGraph, TPoint, TConfigPoint> : IGraphProvider<TGraph, TPoint>
        where TGraph : GraphBase<TPoint>, new()
        where TPoint : GraphPointBase, new()
        where TConfigPoint : IGraphPointConfig<TConfigPoint>, new()
    {
        protected readonly GraphProviderConfig<TConfigPoint> ProviderConfig;

        private TGraph _currentGraph;
        private GraphConfig<TConfigPoint> _currentGraphConfig = new GraphConfig<TConfigPoint>();
        private Dictionary<string, TGraph> _loadedGraphs = new Dictionary<string, TGraph>();

        protected GraphProviderBase(GraphProviderConfig<TConfigPoint> config)
        {
            ProviderConfig = config;
        }

        public TGraph GetCurrentGraph()
        {
            if (!(_currentGraph is null)) //return current graph
                return _currentGraph;

            if (!(_currentGraphConfig is null)) //create from config set current and return current
            {
                var graph = CreateFromConfig(_currentGraphConfig);
                graph.GraphModified += GraphOnGraphModified;
                _loadedGraphs.Add(graph.Info.Name, graph);
                _currentGraph = graph;
            }

            return null;
        }

        public void SetCurrentGraph(TGraph graph)
        {
            if (!Equals(_currentGraph, graph)) _currentGraph = graph;
        }

        public TGraph GetGraph(string graphName)
        {
            if (_loadedGraphs.ContainsKey(graphName)) return _loadedGraphs[graphName];

            if (ProviderConfig.Graphs.ContainsKey(graphName))
            {
                var graphConfig = ProviderConfig.Graphs[graphName];

                return CreateFromConfig(graphConfig);
            }

            throw new GraphProviderException($"Graph:{graphName} not found in repository");
        }

        public IList<ProfileInfo> GetGraphInfos()
        {
            var infos = new List<ProfileInfo>();
            foreach (var graphConfig in ProviderConfig.Graphs.Values) infos.Add(graphConfig.Info);
            return infos;
        }

        public virtual void AddGraph(TGraph graph)
        {
            if (string.IsNullOrEmpty(graph.Info.Key))
                throw new GraphNotConfiguredException(nameof(graph.Info.Key), "Property is null or empty");
            var key = graph.Info.Key;

            if (ProviderConfig.Graphs.ContainsKey(key))
                return;

            var graphConfig = new GraphConfig<TConfigPoint>();
            PopulateConfigFromGraph(ref graphConfig, graph);

            ProviderConfig.Graphs.Add(key, graphConfig);

            _loadedGraphs.Add(key, graph);
            graph.GraphModified += GraphOnGraphModified;
        }

        private void GraphOnGraphModified(object? sender, EventArgs e)
        {
            if (sender is TGraph graph)
            {
                var key = graph.Info.Key;
                graph.Info.ModifiedTime = DateTime.Now;
                var config = ProviderConfig.Graphs[key];
                PopulateConfigFromGraph(ref config, graph);
                ProviderConfig.Graphs[key] = config;
            }
        }

        public virtual void RemoveGraph(string key)
        {
            if (_loadedGraphs.ContainsKey(key))
            {
                _loadedGraphs[key].GraphModified -= GraphOnGraphModified;
                _loadedGraphs.Remove(key);
            }

            if (ProviderConfig.Graphs.ContainsKey(key))
                ProviderConfig.Graphs.Remove(key);
        }

        public virtual TGraph CreateGraph(string key)
        {
            if (ProviderConfig.Graphs.ContainsKey(key))
                throw new GraphProviderException($"this key:{key} already exist.");

            var graph = new TGraph();
            graph.Info.Key = key;
            graph.Info.Name = key;
            graph.Info.CreationTime = DateTime.Now;
            graph.Info.ModifiedTime = DateTime.Now;
            graph.GraphModified += GraphOnGraphModified;

            var config = new GraphConfig<TConfigPoint>();
            PopulateConfigFromGraph(ref config, graph);

            _loadedGraphs.Add(key, graph);
            ProviderConfig.Graphs.Add(key, config);
            return graph;
        }

        protected abstract TGraph CreateFromConfig(GraphConfig<TConfigPoint> config);

        protected abstract GraphConfig<TConfigPoint> PopulateConfigFromGraph(
            ref GraphConfig<TConfigPoint> config, TGraph graph);

        public string GetValidKey()
        {
            string baseName = "Graph";
            string retValue = "";
            for (int i = 0; i < int.MaxValue; i++)
            {
                if (!ProviderConfig.Graphs.ContainsKey(baseName + i))
                {
                    retValue = baseName + i;
                    break;
                }
            }
            return retValue;
        }

        public bool ContainsKey(string key)
        {
            return ProviderConfig.Graphs.ContainsKey(key);
        }
    }
}