using Clima.Basics.Configuration;
using Clima.Basics.Services;
using Clima.Configuration.FileSystem;
using Clima.Core.DataModel.GraphModel;
using Clima.FSGrapRepository;
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

            for (int i = 1; i < 11; i += 2)
            {
                graph.AddPoint(new ValueByDayPoint()
                {
                    DayNumber = i,
                    Value = ((float)i * 10 / 100)
                });
            }
            
            _configurationStorage.Save();
            Assert.Pass();
        }
    }
}