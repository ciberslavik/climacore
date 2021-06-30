using Clima.Core.Alarm;
using Clima.NewtonSoftJsonSerializer;
using Clima.Services.Configuration;
using Clima.Services.Devices;
using Clima.Services.Devices.Configs;
using Clima.Services.Devices.FactoryConfigs;
using Clima.Services.IO;
using FakeIOService;
using NSubstitute;
using NUnit.Framework;

namespace Clima.Services.Tests.Devices
{
    public class DeviceFactory_Tests
    {
        private IIOService _ioService;
        private IConfigurationStorage _configStore;
        private IAlarmManager _alarmManager;

        private IDeviceFactory _factory;

        [SetUp]
        public void FactorySetup()
        {
            _ioService = new FakeIO();
            _ioService.Init();
            
            IFileSystem fs = new DefaultFileSystem();
            IConfigurationSerializer serializer = new NewtonsoftConfigSerializer();
            _configStore = new DefaultConfigurationStorage(serializer, fs);
            _alarmManager = Substitute.For<IAlarmManager>();
            
            CreateDefaultFactoryConfig();
            
            _factory = new DeviceFactory(_configStore, _ioService, _alarmManager);
        }

        private void CreateDefaultFactoryConfig()
        {
            DeviceFactoryConfig factoryConfig = new DeviceFactoryConfig();
            var relayConfig = new RelayConfig()
            {
                EnablePinName = "DO:1:1",
                EnableLevel = ActiveLevel.High,

                MonitorPinName = "DI:1:1",
                MonitorLevel = ActiveLevel.High,

                RelayName = "REL:1",
                
                MonitorTimeout = 200
            };
            factoryConfig.RelayConfigItems.Add(relayConfig);
            
            _configStore.RegisterConfig("DeviceFactory", factoryConfig);
        }
        public DeviceFactory_Tests()
        {
            
        }
        
        
        
        [Test]
        public void CreateFC_Test()
        {
            var fc = _factory.CreateFrequencyConverter(0);
            
            fc.StartFC();
        }

        [Test]
        public void CreateRelay_Test()
        {
            var relay = _factory.GetRelay("REL:1");
            
            relay.On();
            
            relay.Off();
        }
    }
}