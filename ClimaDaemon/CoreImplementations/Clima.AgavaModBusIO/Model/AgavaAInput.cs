using System;
using Clima.Core;
using Clima.Core.IO;

namespace Clima.AgavaModBusIO.Model
{
    public class AgavaAInput : AgavaPinBase, IAnalogInput
    {
        private AgavaAnalogInType _inputType;
        private IAnalogValueConverter _valueConverter;
        private float _value;
        private float _rawValue;

        public AgavaAInput(byte moduleAddress, int pinNumberInModule)
        {
            ModuleId = moduleAddress;
            PinNumberInModule = pinNumberInModule;
            RegAddress = (ushort) (10004 + pinNumberInModule);
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

        public float Value => _value;

        public double RawValue => _rawValue;

        protected virtual void OnValueChanged(AnalogPinValueChangedEventArgs ea)
        {
            ValueChanged?.Invoke(ea);
        }

        public override PinType PinType => PinType.Analog;
        public override PinDir Direction => PinDir.Input;

        public void SetRawValue(in ushort[] value)
        {
            
            if (ValueConverter != null)
            {
                var newValue = ValueConverter.ConvertTo(value);
                //Console.WriteLine($"{PinName}:{value[0]}:{value[1]} - {newValue}");
                if (!newValue.Equals(_value))
                {
                    var prevValue = _value;
                    _value = (float)newValue;
                    OnValueChanged(new AnalogPinValueChangedEventArgs(this, prevValue, _value));
                }
            }
        }
    }
}