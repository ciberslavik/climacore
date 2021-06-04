using System;
using System.Runtime.CompilerServices;
using Clima.Services.IO;

namespace Clima.AgavaModBusIO
{
    public class AgavaDInput : DiscreteInput
    {
        private int _pinNumber;
        private int _regAddress;
        public AgavaDInput()
        {
            _pinNumber = 0;
            _regAddress = 0;
        }

        public AgavaDInput(int pinNumber, int regAddress)
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
