using System.Collections.Generic;
using System;
using System.IO.Ports;
using System.Linq;
using Clima.AgavaModBusIO.Configuration;
using Clima.AgavaModBusIO.Model;
using Clima.Basics.Configuration;
using Clima.Core.IO;
using NModbus;
using NModbus.Serial;


namespace Clima.AgavaModBusIO
{
    public class AgavaIoService:IIOService
    {
        private readonly IConfigurationStorage _configStorage;
        private ModbusConfig _config;
        private SerialPort _port;
        private IModbusSerialMaster _master;
        private IOPinCollection _pins;
        private AgavaWorker _worker;
        private Dictionary<byte, AgavaIOModule> _modules;
        public AgavaIoService(IConfigurationStorage configStorage)
        {
            _configStorage = configStorage;
            _pins = new IOPinCollection();
            _modules = new Dictionary<byte, AgavaIOModule>();
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
            
            _config = _configStorage.GetConfig<ModbusConfig>("ModbusConfig");
            
            _port = new SerialPort();
            _port.PortName = _config.PortName;
            _port.BaudRate = _config.Baudrate;
            
            Console.WriteLine($"Starting Modbus server on port:{_config.PortName}");
            
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
            transport.ReadTimeout = _config.ResponseTimeout;
            transport.WriteTimeout = _config.ResponseTimeout;
            
            _master = factory.CreateMaster(transport);
            
            Console.WriteLine("Start scanning IO bus.");
            ScanBus(1, 5);
            
            Console.WriteLine("Create modbus worker.");
            
            _worker = new AgavaWorker(_modules, _master);
            
            Console.WriteLine("IO System  initialization complete");
            IsInit = true;
        }
        public void Start()
        {
            if (IsInit)
            {
                Console.WriteLine("Starting IO Server.");
                _worker.CycleTime = _config.IOProcessorCycleTime;
                _worker.StartWorker();
            }
        }
        public void Stop()
        {

        }

        public bool IsInit { get; private set; }

        private void ScanBus(byte startAddress, int endAddress)
        {
            for (byte i = startAddress; i <= endAddress; i++)
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
                    var module = AgavaIOModule.CreateModule(i, value);
                    module.AnalogOutputChanged+= ModuleOnAnalogOutputChanged;
                    _modules.Add(i, module);
                }
                catch (TimeoutException)
                {
                    Console.WriteLine($"Address:{i} is free.");
                }
            }
        }
        
        private void ModuleOnAnalogOutputChanged(AnalogPinValueChangedEventArgs ea)
        {
            if (ea.Pin is AgavaAOutput pin)
            {
                AgavaRequest request = new AgavaRequest();
                request.RequestType = RequestType.WriteMultipleRegisters;
                request.ModuleID = pin.ModuleId;
                request.RegisterAddress = pin.RegAddress;
                request.DataCount = 2;
                //TODO
                request.Data = new ushort[2].Select(b => (object)b).ToArray();
                
                _worker.EnqueueRequest(request);
            }

                
        }
    }
}
