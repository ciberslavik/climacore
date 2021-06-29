using System;
using Clima.Services.IO;

namespace Clima.AgavaModBusIO
{
    public class AgavaAOutput:AnalogOutput
    {
        private int _moduleAddr;
        private int _regAddress;
        private int _pinNumberInModule;

        public AgavaAOutput(int moduleAddress, int pinNumberInModule)
        {
            _moduleAddr = moduleAddress;
            _regAddress = pinNumberInModule * 2;
            _pinNumberInModule = pinNumberInModule;
        }
        internal int PinNumberInModule => _pinNumberInModule;
        internal int RegAddress => _regAddress;
        internal int ModuleAddress => _moduleAddr;
    }
}
