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
        private Dictionary<string, AnalogOutput> _analogOutputs;
        private Dictionary<string, AnalogInput> _analogInputs;
        private Dictionary<string, DiscreteInput> _discreteInputs;
        private Dictionary<string, DiscreteOutput> _discreteOutputs;
        public AgavaIoService(IConfigurationStorage configStorage)
        {
            _configStorage = configStorage;
        }

        public bool IsRunning { get; private set; }
        public IDictionary<string, DiscreteInput> DiscreteInputs => _discreteInputs;

        public IDictionary<string, DiscreteOutput> DiscreteOutputs => _discreteOutputs;

        public IDictionary<string, AnalogOutput> AnalogOutputs => _analogOutputs;

        public IDictionary<string, AnalogInput> AnalogInputs => _analogInputs;

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
            //_port.Handshake = Handshake.RequestToSend;
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
                }
                catch (TimeoutException tie)
                {
                    Console.WriteLine($"Address:{i} is free.");
                }
            }
        }

        private void ProcessModule(int moduleId, ushort[] signature)
        {
            
        }
    }
}
