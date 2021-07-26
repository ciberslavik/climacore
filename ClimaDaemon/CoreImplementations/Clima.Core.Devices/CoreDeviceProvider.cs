using System;
using System.Collections.Generic;
using Clima.Basics.Configuration;
using Clima.Core.Devices.Configuration;
using Clima.Core.IO;

namespace Clima.Core.Devices
{
    public class CoreDeviceProvider:IDeviceProvider
    {
        private readonly IConfigurationStorage _configStorage;
        private readonly IIOService _ioService;
        private DeviceProviderConfig _config;
        
        private Dictionary<string, IRelay> _relays;
        private Dictionary<string, IFrequencyConverter> _fcs;
        public CoreDeviceProvider(IIOService ioService)
        {
            _configStorage = ClimaContext.Current.ConfigurationStorage;
            _ioService = ioService ?? throw new ArgumentNullException(nameof(ioService));
            
            if(_configStorage.Exist(DeviceProviderConfig.FileName))
                _config = _configStorage.GetConfig<DeviceProviderConfig>(DeviceProviderConfig.FileName);
            else
            {
                _config = DeviceProviderConfig.CreateDefault();
                _configStorage.RegisterConfig(DeviceProviderConfig.FileName, _config);
            }
            _relays = new Dictionary<string, IRelay>();
            _fcs = new Dictionary<string, IFrequencyConverter>();
        }


        public IRelay GetRelay(string relayName)
        {
            if (_relays.ContainsKey(relayName))
            {
                return _relays[relayName];
            }
            else if (_config.MonitoredRelays.ContainsKey(relayName))
            {
                var relay = new MonitoredRelay(new DefaultTimer());
                relay.Configuration = _config.MonitoredRelays[relayName];
                
                return relay;
            }
            else
            {
                throw new KeyNotFoundException(relayName);
            }
        }

        public IFrequencyConverter GetFrequencyConverter(string converterName)
        {
            if (_fcs.ContainsKey(converterName))
                return _fcs[converterName];

            if (_config.FrequencyConverters.ContainsKey(converterName))
            {
                var converterConfig = _config.FrequencyConverters[converterName];

                IFrequencyConverter converter = default;
                if (converterConfig.ConverterType == ConverterType.Frequency)
                {
                    var dev = new FrequencyConverter();
                    dev.EnablePin = _ioService.Pins.DiscreteOutputs[converterConfig.EnablePinName];
                    dev.AlarmPin = _ioService.Pins.DiscreteInputs[converterConfig.AlarmPinName];
                    dev.AnalogPin = _ioService.Pins.AnalogOutputs[converterConfig.AnalogPinName];

                    converter = dev;
                }
                else if (converterConfig.ConverterType == ConverterType.Thyristor)
                {
                    var dev = new ThyristorConverter();
                    dev.Configuration = converterConfig;
                    dev.AnalogPin = _ioService.Pins.AnalogOutputs[converterConfig.AnalogPinName];

                    converter = dev;
                }
                return converter;
            }

            throw new KeyNotFoundException(converterName);
        }
    }
}