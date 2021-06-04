using System;
using System.Collections.Generic;
using Clima.Services.IO;

namespace Clima.AgavaModBusIO.Model
{
    public class AgavaIoModule
    {
        private Dictionary<string, PinBase> _pins;
        private int _moduleAddress;
        private int _diCount = 0;
        private int _doCount = 0;
        private int _aiCount = 0;
        private int _aoCount = 0;

        private AgavaIoModule(int moduleAddress)
        {
            _moduleAddress = moduleAddress;
            _pins = new Dictionary<string, PinBase>();
        }

        public Dictionary<string, PinBase> Pins
        {
            get => _pins;
            set => _pins = value;
        }

        public static AgavaIoModule CreateIoModule(int address, byte[] signature)
        {
            var module = new AgavaIoModule(address);
            module._moduleAddress = address;

            for (int i = 0; i < 6; i++)
            {
                AgavaSubModuleType smType = (AgavaSubModuleType) signature[i];

                switch (smType)
                {
                    case AgavaSubModuleType.None:
                        break;
                    case AgavaSubModuleType.DO:
                        break;
                    case AgavaSubModuleType.SYM:
                        break;
                    case AgavaSubModuleType.R:
                        for (int j = 0; j < 2; j++)
                        {
                            AgavaDOutput output = module.CreateDigitalOutput();
                            string pinName = $"[DO:{module._moduleAddress}:{output.PinNumber}]";
                            module._pins.Add(pinName, output);
                        }

                        break;
                    case AgavaSubModuleType.AI:
                        break;
                    case AgavaSubModuleType.AIO:
                        break;
                    case AgavaSubModuleType.DI:
                        for (int j = 0; j < 4; j++)
                        {
                            AgavaDInput input = module.CreateDigitalInput();
                            string pinName = $"[DI:{module._moduleAddress}:{input.PinNumber}]";
                            module._pins.Add(pinName, input);
                        }

                        break;
                    case AgavaSubModuleType.TMP:
                        break;
                    case AgavaSubModuleType.DO6:
                        break;
                    case AgavaSubModuleType.ENI:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return module;
        }


        private AgavaDInput CreateDigitalInput()
        {
            int pinNumber = ++_diCount;
            int regAddress = 10000;
            if (pinNumber > 16)
            {
                regAddress += 1;
            }
            return new AgavaDInput(pinNumber, regAddress);
        }

        private AgavaDOutput CreateDigitalOutput()
        {
            int pinNumber = ++_doCount;
            int regAddress = 10000;
            if (pinNumber > 16)
            {
                regAddress += 1;
            }
            return new AgavaDOutput(pinNumber, regAddress);
        }

        private AgavaAInput CreateAnalogInput()
        {
            int pinNumber = ++_aiCount;
            int regAddress = 10004 + (pinNumber - 1);
            return new AgavaAInput(pinNumber, regAddress);
        }

        private AgavaAOutput CreateAnalogOutput()
        {
            return new AgavaAOutput();
        }
    }
}