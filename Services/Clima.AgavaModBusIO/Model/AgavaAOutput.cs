using System;
using Clima.Services.IO;

namespace Clima.AgavaModBusIO
{
    public class AgavaAOutput:AnalogOutput
    {
        private byte _moduleId;
        private ushort _regAddress;
        private int _pinNumberInModule;

        public AgavaAOutput(byte moduleAddress, int pinNumberInModule)
        {
            _moduleId = moduleAddress;
            _regAddress = (ushort)(pinNumberInModule * 2);
            _pinNumberInModule = pinNumberInModule;
        }
        internal int PinNumberInModule => _pinNumberInModule;
        internal ushort RegAddress => _regAddress;
        internal byte ModuleId => _moduleId;
    }
}
