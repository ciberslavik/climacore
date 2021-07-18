using System;
using Clima.Basics.Configuration;
using Clima.Core.DataModel.Graphs;
using Clima.Serialization.Newtonsoft;
using NUnit.Framework;

namespace Clima.Core.Tests
{
    public class DataModelTests
    {
        private IConfigurationSerializer _serializer;
        [SetUp]
        public void Setup()
        {
            _serializer = new ConfigurationSerializer();
        }

        [Test]
        public void TemperatureGraph_Serialize_Deserialize_Test()
        {
            TemperatureGraph graph = new TemperatureGraph();
            graph.AddPoint(new TemperatureGraphPiont());
            graph.Points[0].DayNumber = 2;
            graph.Points[0].Temperature = 23.2;

            graph.AddPoint(new TemperatureGraphPiont());
            graph.Points[1].DayNumber = 4;
            graph.Points[1].Temperature = 30.2;

            var data = _serializer.Serialize(graph);
            Console.WriteLine(data);

            var graph2 = _serializer.Deserialize<TemperatureGraph>(data);


            Assert.NotNull(graph2);
        }
    }
}