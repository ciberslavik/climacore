using Clima.Core.IO;

namespace Clima.Core.Devices
{
    public class Sensors : ISensors
    {
        private IAnalogInput _frontTempPin;
        private IAnalogInput _rearTempPin;
        private IAnalogInput _outdoorTempPin;
        private IAnalogInput _humidityPin;
        private IAnalogInput _pressurePin;

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

        public double FrontTemperature { get; private set; }
        public double RearTemperature { get; private set; }
        public double OutdoorTemperature { get; private set; }
        public double Humidity { get; private set; }
        public double Pressure { get; private set; }
    }
}