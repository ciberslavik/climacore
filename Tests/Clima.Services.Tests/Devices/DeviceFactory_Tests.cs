using Clima.DataModel.Configurations.IOSystem;
using Clima.NewtonSoftJsonSerializer;
using Clima.Services.Alarm;
using Clima.Services.Configuration;
using Clima.Services.Devices;
using Clima.Services.Devices.Configs;
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
            _ioService = new StubIOService();
            _ioService.Init();
            
            IFileSystem fs = new DefaultFileSystem();
            IConfigurationSerializer serializer = new NewtonsoftConfigSerializer();
            _configStore = new DefaultConfigurationStorage(serializer, fs);
            _alarmManager = Substitute.For<IAlarmManager>();
            
            CreateDefaultConfig();
            
            _factory = new DeviceFactory(_configStore, _ioService, _alarmManager);
        }

        private void CreateDefaultConfig()
        {
            DeviceFactoryConfig factoryConfig = new DeviceFactoryConfig();
            for (int relayNumber = 1; relayNumber <= 8; relayNumber++)
            {
                var relayConfig = new RelayConfig()
                {
                    EnablePinName = $"DO:1:{relayNumber + 2}",
                    EnableLevel = ActiveLevel.High,

                    MonitorPinName = $"DI:1:{relayNumber + 2}",
                    MonitorLevel = ActiveLevel.High,

                    RelayName = $"REL:{relayNumber}",

                    MonitorTimeout = 200
                };
                factoryConfig.RelayConfigItems.Add(relayConfig);
            }

            for (int fcNumber = 1; fcNumber <= 2; fcNumber++)
            {
                var fcConfig = new FrequencyConverterConfig()
                {
                    EnablePinName = $"DO:1:{fcNumber}",
                    AlarmPinName = $"DI:1:{fcNumber}",
                    AnalogPinName = $"AO:1:{fcNumber}",
                    ConverterName = $"FC:{fcNumber}",
                    StartUpTime = 1000
                };
                factoryConfig.FcConfigItems.Add(fcConfig);
            }

            _configStore.RegisterConfig("DeviceFactory", factoryConfig);
        }

        public DeviceFactory_Tests()
        {
            
        }
        
        
        
        [Test]
        public void CreateFC_Test()
        {
            var fc = _factory.GetFrequencyConverter("FC:1");
            
            Assert.NotNull(fc);
        }

        [Test]
        public void CreateRelay_Test()
        {
            var relay = _factory.GetRelay("REL:1");
            
            Assert.NotNull(relay);
        }
    }
}