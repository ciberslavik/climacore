using System;
using Clima.Basics.Configuration;
//using Clima.Core.DataModel.Graphs;
using Clima.Serialization.Newtonsoft;
using NUnit.Framework;

namespace Clima.Core.Scheduler.Tests
{
    public class DataModelTests
    {
        private IConfigurationSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new ConfigurationSerializer();
        }

/*        [Test]
        public void TemperatureGraph_Serialize_Deserialize_Test()
        {
            TemperatureGraphBase graphBase = new TemperatureGraphBase();
            graphBase.AddPoint(new TemperatureGraphPiont());
            graphBase.Points[0].DayNumber = 2;
            graphBase.Points[0].Temperature = 23.2;

            graphBase.AddPoint(new TemperatureGraphPiont());
            graphBase.Points[1].DayNumber = 4;
            graphBase.Points[1].Temperature = 30.2;

            var data = _serializer.Serialize(graphBase);
            Console.WriteLine(data);

            var graph2 = _serializer.Deserialize<TemperatureGraphBase>(data);


            Assert.NotNull(graph2);
        }*/
    }
}