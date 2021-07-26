using System.Collections.Generic;
using System;
using System.IO;
using System.IO.Ports;
using System.Linq;
using Clima.AgavaModBusIO.Configuration;
using Clima.AgavaModBusIO.Model;
using Clima.AgavaModBusIO.Transport;
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
        private IAgavaMaster _master;
        private IOPinCollection _pins;
        private AgavaWorker _worker;
        private Dictionary<byte, AgavaIOModule> _modules;
        private byte _moduleScanCounter;
        private int _stopScanAddres;
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
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
                throw new IOServiceException(e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                throw new IOServiceException(e.Message);
            }

            _master = new AgavaModbusRTUMaster(_port);
            
            _master.ReadTimeout = _config.ResponseTimeout;
            _master.WriteTimeout = _config.ResponseTimeout;
            
            Console.WriteLine("Start scanning IO bus.");
            ScanBus(1, 5);
            
            Console.WriteLine("Create modbus worker.");
            
            _worker = new AgavaWorker(_modules, _master);
            _worker.DiscreteCycleDevider = _config.DiscreteReadCycleDevider;
            _worker.AnalogCycleDevider = _config.AnalogReadCycleDevider;
            
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
            if(_worker.IsRunning)
                _worker.StopWorker();
        }

        public bool IsInit { get; private set; }

        private void ScanBus(byte startAddress, int endAddress)
        {
            
            _master.ReplyReceived+= MasterOnReplyReceived;
            _moduleScanCounter = 1;
            
            _stopScanAddres = endAddress;
            ScanModule(_moduleScanCounter);

            BuildPinsCollection();
        }

        private void ScanModule(byte moduleId)
        {
            Console.WriteLine($"Start scanning address:{moduleId}");
            var request = AgavaRequest.ReadHoldingRegisterRequest(moduleId, 2017, 6);
            _master.WriteRequest(request);
        }
        private void MasterOnReplyReceived(object sender, ReplyReceivedEventArgs ea)
        {
            var reply = ea.Reply;
            if (reply.RequestType == RequestType.ReadHoldingRegisters && reply.RegisterAddress == 2017)
            {
                if (!reply.ReplyTimeout)
                {
                    var signature = "Module signature: ";
                    foreach (var d in reply.Data)
                    {
                        signature += $"{d:X}";
                    }
                    Console.WriteLine(signature);
                    _modules.Add(_moduleScanCounter,AgavaIOModule.CreateModule(_moduleScanCounter, reply.Data));
                }
                else
                {
                    Console.WriteLine($" Address:{reply.ModuleID} timeout...");
                }
                _moduleScanCounter++;
                if(_moduleScanCounter > _stopScanAddres)
                    return;
                
                ScanModule(_moduleScanCounter);
            }
        }

        private void BuildPinsCollection()
        {
            foreach (var module in _modules.Values)
            {
                foreach (var ain in module.Pins.AnalogInputs.Values)
                {
                    _pins.AnalogInputs.Add(ain.PinName, ain);
                }

                foreach (var aout in module.Pins.AnalogOutputs.Values)
                {
                    _pins.AnalogOutputs.Add(aout.PinName,aout);
                }

                foreach (var din in module.Pins.DiscreteInputs.Values)
                {
                    _pins.DiscreteInputs.Add(din.PinName,din);
                }

                foreach (var dout in module.Pins.DiscreteOutputs.Values)
                {
                    _pins.DiscreteOutputs.Add(dout.PinName,dout);
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
                request.Data = new ushort[2];
                
                _worker.EnqueueRequest(request);
            }

                
        }
    }
}
