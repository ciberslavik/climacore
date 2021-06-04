using System;
using Clima.Services.IO;

namespace Clima.AgavaModBusIO
{
    public class AgavaAInput:AnalogInput
    {
        private int _regAddress;
        private int _pinNumber;
        public AgavaAInput(int pinNumber, int regAddress)
        {
            _regAddress = regAddress;
            _pinNumber = pinNumber;
        }

        public int RegAddress
        {
            get => _regAddress;
            set => _regAddress = value;
        }

        public int PinNumber
        {
            get => _pinNumber;
            set => _pinNumber = value;
        }
    }
}
