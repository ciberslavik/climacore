using Clima.Services.IO;

namespace Clima.AgavaModBusIO.Model
{
    public class AgavaAOutput:AgavaPinBase, IAnalogOutput
    {
        private IAnalogValueConverter _valueConverter;
        private double _value;

        public AgavaAOutput(byte moduleAddress, int pinNumberInModule)
        {
            _moduleId = moduleAddress;
            _regAddress = (ushort)(pinNumberInModule * 2);
            _pinNumberInModule = pinNumberInModule;
        }
        public event AnalogPinValueChangedEventHandler ValueChanged;

        public IAnalogValueConverter ValueConverter
        {
            get => _valueConverter;
            set => _valueConverter = value;
        }

        public double Value => _value;

        public void SetValue(double value)
        {
            throw new System.NotImplementedException();
        }

        protected virtual void OnValueChanged(AnalogPinValueChangedEventArgs ea)
        {
            ValueChanged?.Invoke(ea);
        }
    }
}
