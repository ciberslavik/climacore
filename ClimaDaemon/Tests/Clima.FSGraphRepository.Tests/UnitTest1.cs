using System;
using Clima.Basics.Configuration;
using Clima.Basics.Services;
using Clima.Configuration.FileSystem;
using Clima.Core.DataModel.GraphModel;
using Clima.FSGrapRepository;
using Clima.FSGrapRepository.Configuration;
using Clima.Serialization.Newtonsoft;
using NUnit.Framework;

namespace Clima.FSGraphRepository.Tests
{
    public class Tests
    {
        private IConfigurationStorage _configurationStorage;

        [SetUp]
        public void Setup()
        {
            _configurationStorage = new FSConfigurationStorage(new ConfigurationSerializer(), new DefaultFileSystem());
        }

        [Test]
        public void TemperatureGraph_Test()
        {
            IGraphProviderFactory fsProvider = new GraphProviderFactoryFileSystem(_configurationStorage);
            var tempProvider = fsProvider.TemperatureGraphProvider();

            var graph = tempProvider.GetGraph("Лето +30");

            for (var i = 1; i < 11; i += 2)
                graph.AddPoint(new ValueByDayPoint()
                {
                    DayNumber = i,
                    Value = (float) i * 10 / 100
                });

            _configurationStorage.Save();
            Assert.Pass();
        }

        [Test]
        public void GraphProviderConfig_Test()
        {
            IGraphProviderFactory fsProvider = new GraphProviderFactoryFileSystem(_configurationStorage);

            var providerConfig = new GraphProviderConfig<TemperatureGraphPointConfig>();


            var graphConfig = new GraphConfig<TemperatureGraphPointConfig>();
            graphConfig.Info.Key = graphConfig.Info.Name = "TestGraph";
            FillGraph(ref graphConfig);
            providerConfig.Graphs.Add(graphConfig.Info.Key, graphConfig);


            var graph2 = new GraphConfig<TemperatureGraphPointConfig>();
            graph2.Info.Key = graph2.Info.Name = "TestGraph2";
            FillGraph(ref graph2);
            providerConfig.Graphs.Add(graph2.Info.Key, graph2);

            Console.WriteLine("Unsorted:");
            foreach (var point in graphConfig.Points) Console.WriteLine($"   Point day:{point.Day}");
            graphConfig.Points.Sort();

            Console.WriteLine("Sorted:");
            foreach (var point in graphConfig.Points) Console.WriteLine($"   Point day:{point.Day}");

            providerConfig.CurrentGraph = graphConfig.Info.Key;

            _configurationStorage.RegisterConfig(
                "TemperatureProviderConfig", providerConfig);
            _configurationStorage.Save();
            Assert.Pass();
        }

        [Test]
        public void CreateGraph_Test()
        {
            IGraphProviderFactory fsProvider = new GraphProviderFactoryFileSystem(_configurationStorage);
            var tempProvider = fsProvider.TemperatureGraphProvider();

            var graph = tempProvider.CreateGraph("graph0");
            graph.AddPoint(new ValueByDayPoint(1, 15.5f));
            _configurationStorage.Save();

            tempProvider.RemoveGraph("graph0");
            _configurationStorage.Save();
            Assert.IsNotNull(graph);
        }

        [Test]
        public void RemoveGraph_Test()
        {
            IGraphProviderFactory fsProvider = new GraphProviderFactoryFileSystem(_configurationStorage);
            var tempProvider = fsProvider.TemperatureGraphProvider();


            tempProvider.RemoveGraph("graph0");

            _configurationStorage.Save();
        }

        private void FillGraph(ref GraphConfig<TemperatureGraphPointConfig> graphConfig)
        {
            var rnd = new Random();
            for (var i = 0; i < 20; i++)
                graphConfig.Points.Add(new TemperatureGraphPointConfig()
                {
                    Day = rnd.Next(1, 50),
                    Temperature = 10
                });
        }
    }
}