using System;
using System.Collections.Generic;
using System.Threading;
using Clima.Basics;
using Clima.Basics.Services;
using Clima.Core.Alarm;
using Clima.Core.Devices.Configuration;
using Clima.Core.IO;

namespace Clima.Core.Devices
{
    public class Sensors : ISensors,IService, IAlarmSource
    {
        private readonly IIOService _ioService;
        private SensorsConfig _config;
        private IAnalogInput _frontTempPin;
        private IAnalogInput _rearTempPin;
        private IAnalogInput _outdoorTempPin;
        private IAnalogInput _humidityPin;
        private IAnalogInput _pressurePin;
        private IAnalogInput _valve1Pin;
        private IAnalogInput _valve2Pin;
        private Timer _movingAvgTimer;
        private MovingAverageFilter _pressureFilter;
        private float _pressure;
        private bool _isAlarm;
        private IEnumerable<AlarmInfo> _provideAlarms;
        private Type _configType;
        private ServiceState _serviceState;

        public Sensors(IIOService ioService)
        {
            _ioService = ioService;
            _pressureFilter = new MovingAverageFilter(10);
            

            _provideAlarms = new List<AlarmInfo>()
            {
                new AlarmInfo("TFMin", "TFMin"),
                new AlarmInfo("TFMax", "TFMax"),
                new AlarmInfo("TRMin", "TRMin"),
                new AlarmInfo("TRMax", "TRMax"),
                new AlarmInfo("RHMin", "RHMin"),
                new AlarmInfo("RHMax", "RHMax"),
            };
        }

        private void MovingAvgUpdate(object o)
        {
            _pressure = _pressureFilter.Calculate(_pressurePin.Value);
        }

        
        internal IAnalogInput FrontTempPin
        {
            get => _frontTempPin;
            set
            {
                _frontTempPin = value;
                FrontTemperature = _frontTempPin.Value;
                _frontTempPin.ValueChanged += (ea) => { FrontTemperature = ea.NewValue; };
            }
        }

        internal IAnalogInput RearTempPin
        {
            get => _rearTempPin;
            set
            {
                _rearTempPin = value;
                RearTemperature = _rearTempPin.Value;
                _rearTempPin.ValueChanged += (ea) => { RearTemperature = ea.NewValue; };
            }
        }

        internal IAnalogInput OutdoorTempPin
        {
            get => _outdoorTempPin;
            set
            {
                _outdoorTempPin = value;
                OutdoorTemperature = _outdoorTempPin.Value;
                _outdoorTempPin.ValueChanged += (ea) => { OutdoorTemperature = ea.NewValue; };
            }
        }

        internal IAnalogInput HumidityPin
        {
            get => _humidityPin;
            set
            {
                _humidityPin = value;
                Humidity = _humidityPin.Value;
                _humidityPin.ValueChanged += (ea) => { Humidity = ea.NewValue; };
            }
        }

        internal IAnalogInput PressurePin
        {
            get => _pressurePin;
            set
            {
                _pressurePin = value;
                _pressure = _pressurePin.Value;
                //_pressurePin.ValueChanged += (ea) =>
                //{
                //    _pressure = _pressureFilter.Calculate(ea.NewValue);
                //};
            }
        }

        internal IAnalogInput Valve1OSPin
        {
            get => _valve1Pin;
            set
            {
                _valve1Pin = value;
                Valve1 = _valve1Pin.Value;
                _valve1Pin.ValueChanged += (ea => { Valve1 = ea.NewValue; });
            }
        }
        internal IAnalogInput Valve2OSPin
        {
            get => _valve2Pin;
            set
            {
                _valve2Pin = value;
                Valve2 = _valve2Pin.Value;
                _valve2Pin.ValueChanged += (ea => { Valve2 = ea.NewValue; });
            }
        }
        public float FrontTemperature { get; private set; }
        public float RearTemperature { get; private set; }
        public float OutdoorTemperature { get; private set; }
        public float Humidity { get; private set; }

        public float Pressure => _pressure; //Filter.Calculate(_pressurePin.Value);
        
        public float Valve1 { get; private set;  }
        public float Valve2 { get; private set;  }
        public event EventHandler<AlarmEventArgs> Alarm;

        public bool IsAlarm => _isAlarm;

        public IEnumerable<AlarmInfo> ProvideAlarms => _provideAlarms;

        void IAlarmSource.Reset()
        {
            _isAlarm = false;
        }

        public void Start()
        {
            _movingAvgTimer = new Timer(MovingAvgUpdate, null, 3000, 1000);
            _serviceState = ServiceState.Running;
        }

        public void Stop()
        {
            
        }

        public void Init(object config)
        {
            if (config is SensorsConfig cfg)
            {
                _config = cfg;
                
                FrontTempPin = _ioService.Pins.AnalogInputs[_config.FrontTempPinName];
                RearTempPin = _ioService.Pins.AnalogInputs[_config.RearTempPinName];
                OutdoorTempPin = _ioService.Pins.AnalogInputs[_config.OutdoorTempPinName];

                HumidityPin = _ioService.Pins.AnalogInputs[_config.HumidityPinName];
                PressurePin = _ioService.Pins.AnalogInputs[_config.PressurePinName];
                Valve1OSPin = _ioService.Pins.AnalogInputs[_config.Valve1PinName];
                Valve2OSPin = _ioService.Pins.AnalogInputs[_config.Valve2PinName];
                _serviceState = ServiceState.Initialized;
            }
        }

        public Type ConfigType => typeof(SensorsConfig);

        public ServiceState ServiceState => _serviceState;
    }
}