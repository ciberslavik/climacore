using Clima.Core.IO;

namespace Clima.AgavaModBusIO.Model
{
    public class AgavaAInput:AgavaPinBase, IAnalogInput
    {
        
        private AgavaAnalogInType _inputType;
        private IAnalogValueConverter _valueConverter;
        private double _value;
        private double _rawValue;

        public AgavaAInput(byte moduleAddress, int pinNumberInModule)
        {
            ModuleId = moduleAddress;
            RegAddress = (ushort)((pinNumberInModule) * 2);
        }
        public AgavaAnalogInType InputType
        {
            get => _inputType;
            set => _inputType = value;
        }

        public event AnalogPinValueChangedEventHandler ValueChanged;

        public IAnalogValueConverter ValueConverter
        {
            get => _valueConverter;
            set => _valueConverter = value;
        }

        public double Value => _value;

        public double RawValue => _rawValue;

        protected virtual void OnValueChanged(AnalogPinValueChangedEventArgs ea)
        {
            ValueChanged?.Invoke(ea);
        }

        public override PinType PinType => PinType.Analog;
        public override PinDir Direction => PinDir.Input;
    }
}
