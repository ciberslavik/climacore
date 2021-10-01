using System;
using System.ComponentModel;
using Clima.AgavaModBusIO.Transport;
using Clima.Core.IO;

namespace Clima.AgavaModBusIO.Model
{
    public class AgavaAOutput : AgavaPinBase, IAnalogOutput
    {
        private IAnalogValueConverter _valueConverter;
        private float _value;

        public AgavaAOutput(byte moduleAddress, int pinNumberInModule)
        {
            _moduleId = moduleAddress;
            _regAddress = (ushort)(10004 + (pinNumberInModule * 2));
            _pinNumberInModule = pinNumberInModule;
            _value = 0f;
        }

        public event AnalogPinValueChangedEventHandler ValueChanged;

        public IAnalogValueConverter ValueConverter
        {
            get => _valueConverter;
            set => _valueConverter = value;
        }

        public float Value => _value;

        public void SetValue(float value)
        {
            if (_value.Equals(value))
                return;

            var prevValue = _value;
            _value = value;
            IsModified = true;
            OnValueChanged(new AnalogPinValueChangedEventArgs(this, prevValue, _value));
        }

        protected virtual void OnValueChanged(AnalogPinValueChangedEventArgs ea)
        {
            ValueChanged?.Invoke(ea);
        }

        public override PinType PinType => PinType.Analog;
        public override PinDir Direction => PinDir.Output;

        internal bool IsModified { get; set; }
        internal bool IsPinTypeChanged { get; }


        internal AgavaRequest GetWriteValueRequest()
        {
            var request = new AgavaRequest();
            request.ModuleID = _moduleId;
            request.RegisterAddress = _regAddress;
            request.RequestType = RequestType.WriteMultipleRegisters;
            
            if (ValueConverter is not null)
            {
                request.Data = ValueConverter.ConvertFrom(_value);
            }
            else
            {
                request.Data = new ushort[] {0, 0};
            }

            request.DataCount = 2;
            return request;
        }

        internal AgavaRequest GetReadValueRequest()
        {
            var request = new AgavaRequest();

            return request;
        }

        internal AgavaRequest GetWritePinTypeRequest()
        {
            var request = new AgavaRequest();

            return request;
        }

        internal AgavaRequest GetReadPinTypeRequest()
        {
            var request = new AgavaRequest();
            request.ModuleID = _moduleId;
            return request;
        }

        internal AgavaAnalogOutType OutputType { get; set; }
    }
}