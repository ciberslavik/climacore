using System;
using Clima.Basics.Configuration;
using Clima.Basics.Services;
using Clima.Configuration.FileSystem;
using Clima.Core.Controllers.Light;
using Clima.Serialization.Newtonsoft;
using NUnit.Framework;

namespace Clima.FSLightRepository.Tests
{
    public class LightRepositoryTests
    {
        private IConfigurationStorage _configurationStorage;
        [SetUp]
        public void Setup()
        {
            var fs = new DefaultFileSystem();
            var cs = new ConfigurationSerializer();
            
            _configurationStorage = new FSConfigurationStorage(cs, fs);
        }

        [TearDown]
        public void Teardown()
        {
            _configurationStorage.Save();
        }
        [Test]
        public void CreatePreset_Test()
        {
            ILightControllerDataRepo repo = new FSLightControllerRepo(_configurationStorage);
            
            var preset = CreateLightTimerPreset();

            repo.AddPreset(preset);
            
            
            Assert.IsTrue(repo.Count > 0,"exist(p0) return false, ");
        }

        [Test]
        public void RemovePreset_Test()
        {
            ILightControllerDataRepo repo = new FSLightControllerRepo(_configurationStorage);
            
            var preset = CreateLightTimerPreset();

            //Console.WriteLine($"Preset key:{preset.Key}");
            
            repo.RemovePreset(new LightTimerProfile(){Key="LightPreset1"});

            Assert.IsFalse(repo.Exist("LightPreset1"));
        }
        private static LightTimerProfile CreateLightTimerPreset()
        {
            var preset = new LightTimerProfile {Key = "", Name = "Broiler"};
            preset.Days.Add(new LightTimerDay()
            {
                Day = 1,
                Timers =
                {
                    new LightTimerItem()
                    {
                        OnTime = DateTime.Parse("3:15"),
                        OffTime = DateTime.Parse("4:00")
                    }
                }
            });
            
            return preset;
        }
    }
}