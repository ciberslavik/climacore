using System.Collections.Generic;
using System;
using System.IO.Ports;
using Clima.Services.IO;
using Clima.AgavaModBusIO.Model;
using NModbus;
using NModbus.Serial;


namespace Clima.AgavaModBusIO
{
    public class AgavaIoService:IIOService
    {
        private Dictionary<int, AgavaIoModule> _modules;
        private SerialPort _port;
        private IModbusSerialMaster _master;

        public AgavaIoService()
        {
            _modules = new Dictionary<int, AgavaIoModule>();
            
        }

        public IList<DiscreteInput> DiscreteInputs => throw new NotImplementedException();

        public IList<DiscreteOutput> DiscreteOutputs => throw new NotImplementedException();

        public void Init()
        {
            _port = new SerialPort();
            var factory = new ModbusFactory();
            var transport = factory.CreateRtuTransport(_port);
            _master = factory.CreateMaster(transport);
        }
        public void Start()
        {

        }
        public void Stop()
        {

        }
        
    }
}
