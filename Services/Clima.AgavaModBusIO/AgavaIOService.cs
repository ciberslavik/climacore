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
        private Dictionary<int, AgavaIoModule> _modules;
        private SerialPort _port;
        private IModbusSerialMaster _master;

        public AgavaIoService(IConfigurationStorage configStorage)
        {
            _configStorage = configStorage;
            _modules = new Dictionary<int, AgavaIoModule>();
        }

        public IDictionary<string, DiscreteInput> DiscreteInputs => throw new NotImplementedException();

        public IDictionary<string, DiscreteOutput> DiscreteOutputs => throw new NotImplementedException();

        public void Init()
        {
            var config = _configStorage.GetConfig<ModbusConfig>();
            _port = new SerialPort();
            _port.PortName = config.PortName;
            _port.BaudRate = config.Baudrate;
            
            var factory = new ModbusFactory();
            var transport = factory.CreateRtuTransport(_port);
            transport.ReadTimeout = config.ResponseTimeout;
            transport.WriteTimeout = config.ResponseTimeout;
            
            _master = factory.CreateMaster(transport);
            ScanBus(1, 5);
        }
        public void Start()
        {

        }
        public void Stop()
        {

        }

        private void ScanBus(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                try
                {
                    byte addr = BitConverter.GetBytes(i)[0];
                    ushort[] value = _master.ReadHoldingRegisters(addr, 2017, 6);
                    AgavaIoModule module = AgavaIoModule.CreateIoModule(i, value);
                    _modules.Add(i, module);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}
