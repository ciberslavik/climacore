using System;
using System.Collections.Generic;
using Clima.Core.Alarm;
using Clima.Services.Configuration;
using Clima.Services.Devices.Configs;
using Clima.Services.Devices.FactoryConfigs;
using Clima.Services.IO;

namespace Clima.Services.Devices
{
    public class DeviceFactory:IDeviceFactory
    {
        private readonly IIOService _io;
        private readonly IAlarmManager _alarmManager;
        private readonly IConfigurationStorage _configStorage;
        private readonly DeviceFactoryConfig _config;

        private readonly Dictionary<string, Relay> _relays;
        
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
            _relays = new Dictionary<string, Relay>();
        }

        public FrequencyConverter CreateFrequencyConverter(int number)
        {
            
            FCConfig fcCfg = _config.FcConfigItems[number];
            
            var fc = new FrequencyConverter();

            fc.Name = $"FC:{number}";
            //Register fc alarm in alarm manager
            _alarmManager.AddNotifier(fc);
            
            fc.InitDevice();
            
            return fc;
        }

        public Relay GetRelay(string relayName)
        {
            //Check relay is created
            if (_relays.ContainsKey(relayName))
                return _relays[relayName];
            
            //Check relay config is exist
            RelayConfig relCfg = _config.GetRelayConfig(relayName);
            if (relCfg is null)
            {
                throw new IndexOutOfRangeException($"Relay not created.\nCannot find configuration for {relayName}");
            }
            //Check valid pin names in config
            if (!_io.Pins.DiscreteOutputs.ContainsKey(relCfg.EnablePinName))
                throw new IndexOutOfRangeException($"Enable pin: {relCfg.EnablePinName} not find in IO system.");
            if (!_io.Pins.DiscreteInputs.ContainsKey(relCfg.MonitorPinName))
                throw new IndexOutOfRangeException($"Monitor pin:{relCfg.MonitorPinName} not find in IO system");
            
            var rel = new Relay();

            rel.EnablePin = _io.Pins.DiscreteOutputs[relCfg.EnablePinName];
            rel.MonitorPin = _io.Pins.DiscreteInputs[relCfg.MonitorPinName];
            rel.Name = relCfg.RelayName;
            rel.Configuration = relCfg;
            
            _alarmManager.AddNotifier(rel);

            rel.InitDevice();
            _relays.Add(relCfg.RelayName, rel);
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