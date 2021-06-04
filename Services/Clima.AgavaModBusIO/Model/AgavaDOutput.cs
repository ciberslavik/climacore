using System;
using Clima.AgavaModBusIO.Model;
using Clima.Services.IO;

namespace Clima.AgavaModBusIO
{
    public class AgavaDOutput : DiscreteOutput
    {
        private int _pinNumber;
        private int _regAddress;
        public AgavaDOutput()
        {
            _pinNumber = 0;
            _pinNumber = 0;
        }

        public AgavaDOutput(int pinNumber, int regAddress)
        {
            _pinNumber = pinNumber;
            _regAddress = regAddress;
        }
        internal int PinNumber
        {
            get => _pinNumber;
        }

        internal int RegAddress
        {
            get => _regAddress;
        }
    }
}
