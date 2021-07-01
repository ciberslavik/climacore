using System;
using System.Collections.Generic;
using Clima.Services.Alarm;
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
        private readonly Dictionary<string, FrequencyConverter> _frequencyConverters;
        
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
            _frequencyConverters = new Dictionary<string, FrequencyConverter>();
        }

        public FrequencyConverter GetFrequencyConverter(string converterName)
        {
            //Check FC created
            if (_frequencyConverters.ContainsKey(converterName))
                return _frequencyConverters[converterName];
            
            //Check FC config is exist
            FCConfig fcCfg = _config.GetFCConfig(converterName);
            if (fcCfg is null)
                throw new IndexOutOfRangeException(
                    $"Frequency converter: {converterName} not created, converter configuration not found.");
            
            //Check pins
            if (!_io.Pins.DiscreteOutputs.ContainsKey(fcCfg.EnablePinName))
                throw new IndexOutOfRangeException(
                    $"Frequency converter: {converterName} not created, \n Enable pin:{fcCfg.EnablePinName} not found in IO system.");

            if (!_io.Pins.DiscreteInputs.ContainsKey(fcCfg.AlarmPinName))
                throw new IndexOutOfRangeException(
                    $"Frequency converter: {converterName} not created, \n Alarm pin:{fcCfg.AlarmPinName} not found in IO system.");

            if (!_io.Pins.AnalogOutputs.ContainsKey(fcCfg.AnalogPinName))
                throw new IndexOutOfRangeException(
                    $"Frequency converter: {converterName} not created, \n Enable pin:{fcCfg.AnalogPinName} not found in IO system.");
            
            var fc = new FrequencyConverter();

            fc.EnablePin = _io.Pins.DiscreteOutputs[fcCfg.EnablePinName];
            fc.AlarmPin = _io.Pins.DiscreteInputs[fcCfg.AlarmPinName];
            fc.AnalogPin = _io.Pins.AnalogOutputs[fcCfg.AnalogPinName];
            
            fc.Name = fcCfg.FCName;
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
            
            var rel = new Relay(new DefaultTimer());

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