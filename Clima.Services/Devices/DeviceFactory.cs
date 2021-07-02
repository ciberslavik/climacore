using System;
using System.Collections.Generic;
using Clima.DataModel.Configurations.IOSystem;
using Clima.Services.Alarm;
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
            FrequencyConverterConfig frequencyConverterCfg = _config.GetFCConfig(converterName);
            if (frequencyConverterCfg is null)
                throw new IndexOutOfRangeException(
                    $"Frequency converter: {converterName} not created, converter configuration not found.");
            
            //Check pins
            if (!_io.Pins.DiscreteOutputs.ContainsKey(frequencyConverterCfg.EnablePinName))
                throw new IndexOutOfRangeException(
                    $"Frequency converter: {converterName} not created, \n Enable pin:{frequencyConverterCfg.EnablePinName} not found in IO system.");

            if (!_io.Pins.DiscreteInputs.ContainsKey(frequencyConverterCfg.AlarmPinName))
                throw new IndexOutOfRangeException(
                    $"Frequency converter: {converterName} not created, \n Alarm pin:{frequencyConverterCfg.AlarmPinName} not found in IO system.");

            if (!_io.Pins.AnalogOutputs.ContainsKey(frequencyConverterCfg.AnalogPinName))
                throw new IndexOutOfRangeException(
                    $"Frequency converter: {converterName} not created, \n Enable pin:{frequencyConverterCfg.AnalogPinName} not found in IO system.");
            
            var fc = new FrequencyConverter();

            fc.EnablePin = _io.Pins.DiscreteOutputs[frequencyConverterCfg.EnablePinName];
            fc.AlarmPin = _io.Pins.DiscreteInputs[frequencyConverterCfg.AlarmPinName];
            fc.AnalogPin = _io.Pins.AnalogOutputs[frequencyConverterCfg.AnalogPinName];
            
            fc.Name = frequencyConverterCfg.ConverterName;
            //Register fc alarm in alarm manager
            _alarmManager.AddNotifier(fc);
            
            fc.InitDevice(frequencyConverterCfg);
            
            _frequencyConverters.Add(frequencyConverterCfg.ConverterName, fc);
            return fc;
        }

        public Relay GetRelay(string relayName)
        {
            //Check relay is created
            if (_relays.ContainsKey(relayName))
                return _relays[relayName];
            
            //Check relay config is exist
            RelayConfig relayConfig = _config.GetRelayConfig(relayName);
            if (relayConfig is null)
            {
                throw new IndexOutOfRangeException($"Relay not created.\nCannot find configuration for {relayName}");
            }
            //Check valid pin names in config
            if (!_io.Pins.DiscreteOutputs.ContainsKey(relayConfig.EnablePinName))
                throw new IndexOutOfRangeException($"Enable pin: {relayConfig.EnablePinName} not find in IO system.");
            if (!_io.Pins.DiscreteInputs.ContainsKey(relayConfig.MonitorPinName))
                throw new IndexOutOfRangeException($"Monitor pin:{relayConfig.MonitorPinName} not find in IO system");
            
            var rel = new Relay(new DefaultTimer());

            rel.EnablePin = _io.Pins.DiscreteOutputs[relayConfig.EnablePinName];
            rel.MonitorPin = _io.Pins.DiscreteInputs[relayConfig.MonitorPinName];
            rel.Name = relayConfig.RelayName;
            rel.Configuration = relayConfig;
            
            _alarmManager.AddNotifier(rel);

            rel.InitDevice(relayConfig);
            _relays.Add(relayConfig.RelayName, rel);
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