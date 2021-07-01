using Clima.Core.Ventelation;
using Clima.NewtonSoftJsonSerializer;
using Clima.Services;
using Clima.Services.Alarm;
using Clima.Services.Configuration;
using Clima.Services.Devices;
using Clima.Services.Devices.Configs;
using Clima.Services.Devices.FactoryConfigs;
using Clima.Services.IO;
using FakeIOService;
using NSubstitute;
using NUnit.Framework;

namespace Clima.Core.Tests.Ventelation
{
    public class VentilationController_Tests
    {
        private IIOService _ioService;
        private IConfigurationStorage _configStore;
        private IAlarmManager _alarmManager;

        private IDeviceFactory _factory;

        [SetUp]
        public void TestsSetUp()
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
                    EnablePinName = $"DO:1:{relayNumber+2}",
                    EnableLevel = ActiveLevel.High,

                    MonitorPinName = $"DI:1:{relayNumber+2}",
                    MonitorLevel = ActiveLevel.High,

                    RelayName = $"REL:{relayNumber}",

                    MonitorTimeout = 200
                };
                factoryConfig.RelayConfigItems.Add(relayConfig);
            }

            for (int fcNumber = 1; fcNumber <= 2; fcNumber++)
            {
                var fcConfig = new FCConfig()
                {
                    EnablePinName = $"DO:1:{fcNumber}",
                    AlarmPinName = $"DI:1:{fcNumber}",
                    AnalogPinName = $"AO:1:{fcNumber}",
                    FCName = $"FC:{fcNumber}",
                    StartUpTime = 1000
                };
                factoryConfig.FcConfigItems.Add(fcConfig);
            }

            _configStore.RegisterConfig("DeviceFactory", factoryConfig);

            VentControllerConfig ventConfig = new VentControllerConfig();

            for (int dFanNumber = 1; dFanNumber <= 8; dFanNumber++)
            {
                var dFanConfig = new DiscreteFanConfig()
                {
                    PerformancePerFan = 15000,
                    FanCount = 2,
                    IsDiscard = false,
                    RelayName = $"REL:{dFanNumber}",
                    StartPower = 50,
                    StopPower = 10,
                    FanName = $"Discrete fan {dFanNumber}"
                };
                ventConfig.DiscreteFanConfigs.Add(dFanConfig);
            }

            for (int aFanNumber = 1; aFanNumber <= 2; aFanNumber++)
            {
                var aFanConfig = new AnalogFanConfig()
                {
                    Performance = 15000,
                    FanName = $"Analog fan {aFanNumber}",
                    FCName = $"FC:{aFanNumber}",
                    MinimumPower = 10
                };
                ventConfig.AnalogFanConfigs.Add(aFanConfig);
            }

            _configStore.RegisterConfig("VentilationController", ventConfig);
        }

        [Test]
        public void CreateController_Test()
        {
            var controllerConfig = _configStore.GetConfig<VentControllerConfig>("VentilationController");
            var ventController = new VentilationController(_factory);
            
            ventController.Init(controllerConfig);
            
            Assert.NotNull(ventController);
        }
    }
}