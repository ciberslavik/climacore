using System.Collections.Generic;
using System;
using System.IO.Ports;
using Clima.AgavaModBusIO.Configuration;
using Clima.Services.IO;
using Clima.AgavaModBusIO.Model;
using Clima.Services.Configuration;
using NModbus;
using NModbus.Serial;


namespace Clima.AgavaModBusIO
{
    public class AgavaIoService:IIOService
    {
        private readonly IConfigurationStorage _configStorage;
        
        private SerialPort _port;
        private IModbusSerialMaster _master;
        private IOPinCollection _pins;
        public AgavaIoService(IConfigurationStorage configStorage)
        {
            _configStorage = configStorage;
            _pins = new IOPinCollection();
        }

        public bool IsRunning { get; private set; }

        public IOPinCollection Pins => _pins;


        public void Init()
        {
            if (!_configStorage.Exist("ModbusConfig"))
            {
                var defaultConfig = ModbusConfig.CreateDefault();
                _configStorage.RegisterConfig("ModbusConfig", defaultConfig);
            }
            
            var config = _configStorage.GetConfig<ModbusConfig>("ModbusConfig");
            
            _port = new SerialPort();
            _port.PortName = config.PortName;
            _port.BaudRate = config.Baudrate;
            
            Console.WriteLine($"Starting Modbus server on port:{config.PortName}");
            
            try
            {
                _port.Open();
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e);
                throw new IOServiceException(e.Message);
            }
            
            
            var factory = new ModbusFactory();
            var transport = factory.CreateRtuTransport(_port);
            transport.ReadTimeout = config.ResponseTimeout;
            transport.WriteTimeout = config.ResponseTimeout;
            
            _master = factory.CreateMaster(transport);
            
            ScanBus(1, 5);
            IsInit = true;
        }
        public void Start()
        {

        }
        public void Stop()
        {

        }

        public bool IsInit { get; private set; }

        private void ScanBus(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                try
                {
                    Console.WriteLine($"Scan address:{i}");
                    
                    byte addr = BitConverter.GetBytes(i)[0];
                    ushort[] value = _master.ReadHoldingRegisters(addr, 2017, 6);
                    string signatureStr = $"Module:{i} signature:";
                    foreach (var mod in value)
                    {
                        signatureStr += $"{mod:X}";
                    }
                    Console.WriteLine(signatureStr);
                    ProcessModule(i, value);
                }
                catch (TimeoutException tie)
                {
                    Console.WriteLine($"Address:{i} is free.");
                }
            }
        }

        internal void ProcessModule(int moduleId, ushort[] signature)
        {
            int mDICount = 0;
            int mDOCount = 0;
            int mAICount = 0;
            int mAOCount = 0;

            for (int subNumber = 0; subNumber < signature.Length; subNumber++)
            {
                AgavaSubModuleType subType = (AgavaSubModuleType)signature[subNumber];
                
                switch (subType)
                {
                    case AgavaSubModuleType.None:
                        continue;
                    case AgavaSubModuleType.DO:
                        for (int p = 0; p < 4; p++)
                        {
                            mDOCount++;
                            CreateDiscrOut(moduleId, mDOCount);
                        }
                        break;
                    case AgavaSubModuleType.SYM:
                        for (int p = 0; p < 2; p++)
                        {
                            mDOCount++;
                            CreateDiscrOut(moduleId, mDOCount);
                        }
                        break;
                    case AgavaSubModuleType.R:
                        for (int p = 0; p < 2; p++)
                        {
                            mDOCount++;
                            CreateDiscrOut(moduleId, mDOCount);
                        }
                        break;
                    case AgavaSubModuleType.AI:
                        for (int p = 0; p < 4; p++)
                        {
                            mAICount++;
                            CreateAnalogIn(moduleId, mAICount);
                        }
                        break;
                    case AgavaSubModuleType.AIO:
                        for (int p = 0; p < 2; p++)
                        {
                            mAICount++;
                            CreateAnalogIn(moduleId, mAICount);
                        }
                        for (int p = 0; p < 2; p++)
                        {
                            mAOCount++;
                            CreateAnalogOut(moduleId, mAOCount);
                        }
                        break;
                    case AgavaSubModuleType.DI:
                        for (int p = 0; p < 4; p++)
                        {
                            mDICount++;
                            CreateDiscrIn(moduleId, mDICount);
                        }
                        break;
                    case AgavaSubModuleType.TMP:
                        for (int p = 0; p < 2; p++)
                        {
                            mAICount++;
                            CreateAnalogIn(moduleId, mAICount);
                        }
                        break;
                    case AgavaSubModuleType.DO6:
                        for (int p = 0; p < 6; p++)
                        {
                            mDOCount++;
                            CreateDiscrOut(moduleId, mDOCount);
                        }
                        break;
                    case AgavaSubModuleType.ENI:
                        continue;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        #region Create pins methods
        private void CreateDiscrOut(int moduleId, int mDOCount)
        {
            var pinName = $"DO:{moduleId}:{mDOCount}";
            var DO = new AgavaDOutput(moduleId, mDOCount)
            {
                PinName = pinName
            };
            DO.PinStateChanged += DOOnPinStateChanged;
            
            _pins.AddDiscreteOutput(pinName, DO);
        }
        private void CreateDiscrIn(int moduleId, int mDOCount)
        {
            var pinName = $"DI:{moduleId}:{mDOCount}";
            var DI = new AgavaDInput(moduleId, mDOCount)
            {
                PinName = pinName
            };
            

            _pins.AddDiscreteInput(pinName, DI);
        }
        private void CreateAnalogIn(int moduleId, int pinNumber)
        {
            var pinName = $"AI:{moduleId}:{pinNumber}";
            var AI = new AgavaAInput(moduleId, pinNumber)
            {
                PinName = pinName
            };
            _pins.AddAnalogInput(pinName, AI);
        }
        private void CreateAnalogOut(int moduleId, int pinNumber)
        {
            var pinName = $"AO:{moduleId}:{pinNumber}";
            var AO = new AgavaAOutput(moduleId, pinNumber)
            {
                PinName = pinName
            };
            AO.ValueChanged += AnalogOutValueChanged;
            _pins.AddAnalogOutput(pinName, AO);
        }
        #endregion Create pins methods
        private void AnalogOutValueChanged(AnalogPinValueChangedEventArgs ea)
        {
            
        }

        private void DOOnPinStateChanged(DiscretePinStateChangedEventArgs args)
        {
            if (args.Pin is AgavaDOutput pin)
            {
                Console.WriteLine($"Pin:{pin.PinName} changedt to:{args.NewState}");
            }
        }


        
        
    }
}
