using System;
using System.Runtime.CompilerServices;
using Clima.Services.IO;

namespace Clima.AgavaModBusIO
{
    public class AgavaDInput : DiscreteInput
    {
        private int _pinNumberInModule;
        private int _regAddress;
        private int _moduleAddr;

        public AgavaDInput(int moduleAddr, int pinNumberInModule)
        {
            _pinNumberInModule = pinNumberInModule;
            _regAddress = 10000 + (_pinNumberInModule / 16);
            _moduleAddr = moduleAddr;
        }
        internal int PinNumberInModule => _pinNumberInModule;
        internal int RegAddress => _regAddress;
        internal int ModuleAddress => _moduleAddr;
    }
}
