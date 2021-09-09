using System;
using System.Collections.Generic;
using Clima.Basics;
using Clima.Basics.Configuration;
using Clima.Basics.Services;
using Clima.Core.DataModel;
using Clima.Core.Devices.Configuration;
using Clima.Core.IO;

namespace Clima.Core.Devices
{
    public class CoreDeviceProvider : IDeviceProvider
    {
        private readonly IIOService _ioService;
        private DeviceProviderConfig _config;

        private readonly Dictionary<string, IRelay> _relays = new Dictionary<string, IRelay>();
        private readonly Dictionary<string, IFrequencyConverter> _fcs = new Dictionary<string, IFrequencyConverter>();
        private readonly Dictionary<string, IServoDrive> _servos = new Dictionary<string, IServoDrive>();
        private readonly Dictionary<string, IHeater> _heaters = new Dictionary<string, IHeater>();
        private ISensors _sensors;

        public CoreDeviceProvider(IIOService ioService,IConfigurationStorage configStorage)
        {
            _ioService = ioService;
            if (configStorage.Exist(nameof(DeviceProviderConfig)))
            {
                _config = configStorage.GetConfig<DeviceProviderConfig>();
            }
            else
            {
                var c = DeviceProviderConfig.CreateDefault();
                configStorage.RegisterConfig(c);
                _config = c;
            }
            _sensors = null;
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
                relay.EnablePin = _ioService.Pins.DiscreteOutputs[_config.MonitoredRelays[relayName].ControlPinName];
                relay.MonitorPin = _ioService.Pins.DiscreteInputs[_config.MonitoredRelays[relayName].MonitorPinName];
                _relays.Add(relayName, relay);
                return relay;
            }
            else
            {
                throw new KeyNotFoundException(relayName);
            }
        }

        public List<RelayInfo> GetRelayInfos()
        {
            List<RelayInfo> infos = new List<RelayInfo>();
            foreach (var relayConfig in _config.MonitoredRelays.Values)
            {
                bool curState = false;
                if (_relays.ContainsKey(relayConfig.RelayName))
                    curState = _relays[relayConfig.RelayName].RelayIsOn;
                
                infos.Add(new RelayInfo()
                {
                    Key = relayConfig.RelayName,
                    Name = relayConfig.RelayName,
                    CurrentState = curState
                });
            }

            return infos;
        }

        public IServoDrive GetServo(string servoName)
        {
            if (_servos.ContainsKey(servoName))
                return _servos[servoName];

            if (_config.Servos.ContainsKey(servoName))
            {
                var servoConfig = _config.Servos[servoName];

                var servo = new LinearServo();
                servo.ServoOpenPin = _ioService.Pins.DiscreteOutputs[servoConfig.OpenPinName];
                servo.ServoClosePin = _ioService.Pins.DiscreteOutputs[servoConfig.ClosePinName];
                servo.ServoFeedbackPin = _ioService.Pins.AnalogInputs[servoConfig.FeedbackPinName];
                servo.Logger = Logger;
                servo.Configuration = servoConfig;
                return servo;
            }

            throw new KeyNotFoundException(servoName);
        }

        public IHeater GetHeater(string heaterName)
        {
            if (_heaters.ContainsKey(heaterName))
                return _heaters[heaterName];
            else if (_config.Heaters.ContainsKey(heaterName))
            {
                var heat = new Heater();
                heat.EnablePin = _ioService.Pins.DiscreteOutputs[_config.Heaters[heaterName].PinName];
                _heaters.Add(heaterName, heat);
            }
            
            return _heaters[heaterName];
        }

        public ISensors GetSensors()
        {
            if (!_ioService.IsInit)
                return default;
            
            if (_sensors is null)
            {
                var s = new Sensors();
                s.FrontTempPin = _ioService.Pins.AnalogInputs[_config.Sensors.FrontTempPinName];
                s.RearTempPin = _ioService.Pins.AnalogInputs[_config.Sensors.RearTempPinName];
                s.OutdoorTempPin = _ioService.Pins.AnalogInputs[_config.Sensors.OutdoorTempPinName];

                s.HumidityPin = _ioService.Pins.AnalogInputs[_config.Sensors.HumidityPinName];
                s.PressurePin = _ioService.Pins.AnalogInputs[_config.Sensors.PressurePinName];
                return s;
            }
            else
            {
                return _sensors;
            }
        }

        public ISystemLogger Logger { get; set; }

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

        public IDiscreteFan GetDiscreteFan(string fanName)
        {
            throw new NotImplementedException();
        }

        public IAnalogFan GetAnalogFan(string fanName)
        {
            throw new NotImplementedException();
        }
    }
}