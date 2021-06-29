using System;
using Clima.AgavaModBusIO.Model;
using Clima.Services.IO;

namespace Clima.AgavaModBusIO
{
    public class AgavaAInput:AnalogInput
    {
        private int _moduleAddr;
        private int _regAddress;
        private int _pinNumberInModule;
        private AgavaAnalogInType _inputType;
        public AgavaAInput(int moduleAddress, int pinNumberInModule)
        {
            _moduleAddr = moduleAddress;
            _regAddress = pinNumberInModule * 2;
            _pinNumberInModule = pinNumberInModule;
        }
        internal int PinNumberInModule => _pinNumberInModule;
        internal int RegAddress => _regAddress;
        internal int ModuleAddress => _moduleAddr;

        public AgavaAnalogInType InputType
        {
            get => _inputType;
            set => _inputType = value;
        }
    }
}
