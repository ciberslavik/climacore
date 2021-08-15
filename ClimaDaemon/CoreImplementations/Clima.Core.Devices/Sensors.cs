﻿using Clima.Core.IO;

namespace Clima.Core.Devices
{
    public class Sensors : ISensors
    {
        private IAnalogInput _frontTempPin;
        private IAnalogInput _rearTempPin;
        private IAnalogInput _outdoorTempPin;
        private IAnalogInput _humidityPin;
        private IAnalogInput _pressurePin;
        private IAnalogInput _valve1Pin;
        private IAnalogInput _valve2Pin;

        public Sensors()
        {
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
                Pressure = _humidityPin.Value;
                _pressurePin.ValueChanged += (ea) => { Pressure = ea.NewValue; };
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
        public float Pressure { get; private set; }
        public float Valve1 { get; private set;  }
        public float Valve2 { get; private set;  }
    }
}