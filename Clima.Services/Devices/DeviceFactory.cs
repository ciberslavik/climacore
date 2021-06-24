using System;
using Clima.Core.Alarm;
using Clima.Services.Configuration;
using Clima.Services.Devices.Configs;
using Clima.Services.IO;

namespace Clima.Services.Devices
{
    public class DeviceFactory:IDeviceFactory
    {
        private readonly IIOService _io;
        private readonly IAlarmManager _alarmManager;
        private readonly IConfigurationStorage _configStorage;
        private readonly DeviceFactoryConfig _config;
        
        public DeviceFactory(IConfigurationStorage configStorage, IIOService io,IAlarmManager _alarmManager)
        {
            _configStorage = configStorage;
            if (!_configStorage.Exist("DeviceFactory"))
            {
                var cfg = DeviceFactoryConfig.CreateDefault();
                
                _configStorage.RegisterConfig("DeviceFactory", cfg);
            }

            _io = io;
            this._alarmManager = _alarmManager;
            _config = _configStorage.GetConfig<DeviceFactoryConfig>("DeviceFactory");
        }

        public FrequencyConverter CreateFrequencyConverter(int number)
        {
            
            FCConfig fcCfg = _config.FcConfigItems[number];
            
            var fc = new FrequencyConverter();

            fc.Name = $"FC:{number}";
            //Register fc alarm in alarm manager
            _alarmManager.AddNotifier(fc);
            
            fc.InitDevice(_io, fcCfg);
            
            return fc;
        }

        public Relay CreateRelay(int relayNumber)
        {
            RelayConfig relCfg = _config.RelayConfigItems[relayNumber];
            
            var rel = new Relay();
            rel.Name = $"REL:{relayNumber}";
            
            _alarmManager.AddNotifier(rel);

            rel.InitDevice(_io, relCfg);
            return rel;
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