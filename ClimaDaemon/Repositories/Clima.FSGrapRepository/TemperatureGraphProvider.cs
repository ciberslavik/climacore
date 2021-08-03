using System;
using System.Collections.Generic;
using Clima.Core.DataModel.GraphModel;
using Clima.FSGrapRepository.Configuration;

namespace Clima.FSGrapRepository
{
    //public class TemperatureGraphProvider:IGraphProvider<TemperatureGraph>
    //{
        /*private readonly TemperatureGraphProviderConfig _config;
        private TemperatureGraphConfig _currentGraphConfig;
        private TemperatureGraph _currentGraph;
        private Dictionary<string, TemperatureGraph> _loadedGraphs;

        internal event EventHandler SaveNeeded;
        public TemperatureGraphProvider(TemperatureGraphProviderConfig config)
        {
            _config = config;
            _loadedGraphs = new Dictionary<string, TemperatureGraph>();
        }


        public TemperatureGraph GetCurrentGraph()
        {
            if (_currentGraph is null)
            {
                if (_config.Graphs.ContainsKey(_config.CurrentGraph))
                {
                    var graphConfig = _config.Graphs[_config.CurrentGraph];
                    _currentGraph = CreateGraph(graphConfig);
                }
            }
            return _currentGraph;
        }

        public void SetCurrentGraph()
        {
            throw new System.NotImplementedException();
        }

        public TemperatureGraph GetGraph(string graphName)
        {
            if(!(_currentGraph is null))                    //Если запрошен текущий возвращаем текущий
                if (_currentGraph.Info.Name == graphName)
                    return _currentGraph;

            if (_loadedGraphs.ContainsKey(graphName))      //если он уже загружен
                return _loadedGraphs[graphName];

            if (_config.Graphs.ContainsKey(graphName)) //Или загружаем из конфигурации
            {
                var graph = CreateGraph(_config.Graphs[graphName]);
                _loadedGraphs.Add(graphName, graph);
                return graph;
            }
            else //  если нет конфигурации то создаемновый
            {
                var graph = new TemperatureGraph();
                graph.Info.Name = graphName;
                _loadedGraphs.Add(graphName, graph);
                var graphConfig = CreateConfigFromGraph(graph);
                _config.Graphs.Add(graphName, graphConfig);
                
                return graph;
            }
            
        }

        public IEnumerable<GraphInfo> GetGraphInfos()
        {
            List<GraphInfo> infos = new List<GraphInfo>();
            foreach (var graphConfig in _config.Graphs.Values)
            {
                infos.Add(graphConfig.Info);
            }

            return infos;
        }

        public void AddGraph(string graphName, TemperatureGraph graph)
        {
            if(_config.Graphs.ContainsKey(graphName))
                return;
            
            _config.Graphs.Add(graphName,CreateConfigFromGraph(graph));
            
        }

        public void RemoveGraph(string graphName)
        {
            throw new System.NotImplementedException();
        }

        private TemperatureGraph CreateGraph(TemperatureGraphConfig config)
        {
            var graph = new TemperatureGraph();
            graph.Info = config.Info;

            foreach (var pointConfig in config.Points)
            {
                graph.AddPoint(new ValueByDayPoint()
                {
                    DayNumber = pointConfig.Day,
                    Value = pointConfig.Temperature
                });
            }
            return graph;
        }

        public void Save()
        {
            foreach (var loadedItem in _loadedGraphs)
            {
                if (_config.Graphs.ContainsKey(loadedItem.Key))
                {
                    _config.Graphs[loadedItem.Key] = CreateConfigFromGraph(loadedItem.Value);
                }
                else
                {
                    _config.Graphs.Add(loadedItem.Key, CreateConfigFromGraph(loadedItem.Value));
                }
            }
            SaveNeeded?.Invoke(this, EventArgs.Empty);
        }
        private TemperatureGraphConfig CreateConfigFromGraph(TemperatureGraph graph)
        {
            var config = new TemperatureGraphConfig();
            config.Info = graph.Info;

            foreach (var graphPoint in graph.Points)
            {
                config.Points.Add(new TemperatureGraphPointConfig()
                {
                    Day =  graphPoint.DayNumber,
                    Temperature = graphPoint.Value
                });
            }

            return config;
        }*/
   // }
}