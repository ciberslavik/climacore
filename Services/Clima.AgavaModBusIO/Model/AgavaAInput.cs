using Clima.Services.IO;

namespace Clima.AgavaModBusIO.Model
{
    public class AgavaAInput:AnalogInput
    {
        private byte _moduleId;
        private ushort _regAddress;
        private int _pinNumberInModule;
        private AgavaAnalogInType _inputType;
        public AgavaAInput(byte moduleAddress, int pinNumberInModule)
        {
            _moduleId = moduleAddress;
            _regAddress = (ushort)(pinNumberInModule * 2);
            _pinNumberInModule = pinNumberInModule;
        }
        internal int PinNumberInModule => _pinNumberInModule;
        internal ushort RegAddress => _regAddress;
        internal byte ModuleId => _moduleId;

        public AgavaAnalogInType InputType
        {
            get => _inputType;
            set => _inputType = value;
        }
    }
}
