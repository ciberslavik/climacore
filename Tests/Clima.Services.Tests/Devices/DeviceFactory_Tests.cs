using Clima.Core.Alarm;
using Clima.NewtonSoftJsonSerializer;
using Clima.Services.Configuration;
using Clima.Services.Devices;
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
        public DeviceFactory_Tests()
        {
            _ioService = new FakeIO();
            _ioService.Init();
            
            IFileSystem fs = new DefaultFileSystem();
            IConfigurationSerializer serializer = new NewtonsoftConfigSerializer();
            _configStore = new DefaultConfigurationStorage(serializer, fs);
            _alarmManager = Substitute.For<IAlarmManager>();

            _factory = new DeviceFactory(_configStore, _ioService, _alarmManager);
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
            var relay = _factory.CreateRelay(0);
            
            relay.On();
            
            relay.Off();
        }
    }
}