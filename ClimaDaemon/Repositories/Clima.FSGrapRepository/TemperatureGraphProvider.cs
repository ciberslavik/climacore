﻿using System;
using System.Collections.Generic;
using Clima.Core.DataModel.GraphModel;
using Clima.FSGrapRepository.Configuration;

namespace Clima.FSGrapRepository
{
    public class TemperatureGraphProvider : GraphProviderBase<TemperatureGraph, ValueByDayPoint,
        TemperatureGraphPointConfig>
    {
        public TemperatureGraphProvider(GraphProviderConfig<TemperatureGraphPointConfig> config) : base(config)
        {
            
        }
        protected override TemperatureGraph CreateFromConfig(GraphConfig<TemperatureGraphPointConfig> config)
        {
            var graph = new TemperatureGraph();
            graph.Info = config.Info;

            foreach (var pointConfig in config.Points)
            {
                var point = new ValueByDayPoint(pointConfig.Day, pointConfig.Temperature);
                graph.Points.Add(point);
            }

            return graph;
        }

        protected override GraphConfig<TemperatureGraphPointConfig> PopulateConfigFromGraph(
            ref GraphConfig<TemperatureGraphPointConfig> config, TemperatureGraph graph)
        {
            config.Info = graph.Info;

            foreach (var point in graph.Points)
            {
                var pointConfig = new TemperatureGraphPointConfig(point.DayNumber, point.Value);
                config.Points.Add(pointConfig);
            }

            return config;
        }
        
    }
}