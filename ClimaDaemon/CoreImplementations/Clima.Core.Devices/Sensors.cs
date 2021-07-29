using Clima.Core.IO;

namespace Clima.Core.Devices
{
    public class Sensors:ISensors
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
                _frontTempPin.ValueChanged += (ea) => { FrontTemperature = ea.NewValue; };
            }
        }

        internal IAnalogInput RearTempPin
        {
            get => _rearTempPin;
            set
            {
                _rearTempPin = value;
                _rearTempPin.ValueChanged += (ea) => { RearTemperature = ea.NewValue;};
            }
        }

        internal IAnalogInput OutdoorTempPin
        {
            get => _outdoorTempPin;
            set
            {
                _outdoorTempPin = value;
                _outdoorTempPin.ValueChanged += (ea) => { OutdoorTemperature = ea.NewValue; };
            }

        }

        internal IAnalogInput HumidityPin
        {
            get => _humidityPin;
            set
            {
                _humidityPin = value;
                _humidityPin.ValueChanged += (ea) => { Humidity = ea.NewValue; };
            }
        }

        internal IAnalogInput PressurePin
        {
            get => _pressurePin;
            set
            {
                _pressurePin = value;
                _pressurePin.ValueChanged += (ea) => { Pressure = ea.NewValue; };
            }
        }

        public double FrontTemperature { get; private set; }
        public double RearTemperature { get; private set;}
        public double OutdoorTemperature { get; private set;}
        public double Humidity { get; private set;}
        public double Pressure { get; private set;}
    }
}