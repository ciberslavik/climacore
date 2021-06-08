using System;
using Clima.Services.Configuration;
using Clima.Services.Devices.Configs;
using Clima.Services.IO;

namespace Clima.Services.Devices
{
    public class DeviceFactory:IDeviceFactory
    {
        private readonly IIOService _io;
        private readonly IConfigurationStorage _configStorage;
        private readonly DeviceFactoryConfig _config;
        
        public DeviceFactory(IConfigurationStorage configStorage, IIOService io)
        {
            _configStorage = configStorage;
            if(!_configStorage.Exist<DeviceFactoryConfig>())
                _configStorage.RegisterConfig<DeviceFactoryConfig>();
            
            _io = io;
            _config = _configStorage.GetConfig<DeviceFactoryConfig>();
        }

        public FrequencyConverter CreateFrequencyConverter(int number)
        {
            
            throw new NotImplementedException();
        }

        public DiscreteFanController CreateFanController()
        {
            
            throw new NotImplementedException();
        }

        public ServoController CreateServoController(int number)
        {
            throw new NotImplementedException();
        }

        public HeaterController CreateHeaterController(int number)
        {
            throw new NotImplementedException();
        }
    }
}