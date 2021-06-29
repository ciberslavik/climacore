using System;
using Clima.AgavaModBusIO.Model;
using Clima.Services.IO;

namespace Clima.AgavaModBusIO
{
    public class AgavaDOutput : DiscreteOutput
    {
        private int _pinNumberInModule;
        private int _regAddress;
        private int _moduleAddr;

        internal AgavaDOutput(int moduleAddr, int pinNumberInModule)
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
