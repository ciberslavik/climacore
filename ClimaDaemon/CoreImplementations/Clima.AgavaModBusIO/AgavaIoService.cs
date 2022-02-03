using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.IO;
using System.IO.Ports;
using System.Linq;
using Clima.AgavaModBusIO.Configuration;
using Clima.AgavaModBusIO.Model;
using Clima.AgavaModBusIO.Transport;
using Clima.Basics.Configuration;
using Clima.Basics.Services;
using Clima.Core;
using Clima.Core.IO;
using Clima.Core.IO.Converters;
using NModbus;
using NModbus.Serial;


namespace Clima.AgavaModBusIO
{
    public class AgavaIoService : IIOService
    {
        private ModbusConfig _config;
        private IAgavaMaster _master;
        private IModbusMaster _rtuMaster;

        private IOPinCollection _pins;
        private Dictionary<byte, AgavaIOModule> _modules;
        private Queue<AgavaRequest> _requestQueue;
        private static object _queueLocker = new object();
        private long _cycleCounter;
        private LogFileWriter _log;
        public AgavaIoService()
        {
            _pins = new IOPinCollection();
            _modules = new Dictionary<byte, AgavaIOModule>();
            _requestQueue = new Queue<AgavaRequest>();
            _log = new LogFileWriter("IOLog.log");
            _pins.DiscreteOutputChanged+= PinsOnDiscreteOutputChanged; 
        }

        private void PinsOnDiscreteOutputChanged(DiscretePinStateChangedEventArgs ea)
        {
            _log.WriteLine(ea.Pin.PinName + " - " + ea.NewState);
        }

        public bool IsRunning { get; private set; }
        public ISystemLogger Log { get; set; }
        

        public IOPinCollection Pins => _pins;


        public void Init(object config)
        {
            if (config is ModbusConfig cnf)
                _config = cnf;
            else
                throw new ArgumentException("Configuration is not a ModbusConfig type", nameof(config));

            Log.Info("Start initialize IO Service");
            var port = CreatePort();
            
            var factory = new ModbusFactory();
            var transport = factory.CreateRtuTransport(port);
            _rtuMaster = factory.CreateMaster(transport);
            _rtuMaster.Transport.ReadTimeout = _config.ResponseTimeout;
            _rtuMaster.Transport.WriteTimeout = _config.ResponseTimeout;

            _master = new AgavaModbusRTUMaster(port);

            Log.Debug("Start scanning IO bus.");

            ScanBus(_config.BusStartAddress, _config.BusEndAddress);

            Log.Debug("Read analogInputs");
            ReadAnalogInputs();

            Log.Info($"IO System  initialization complete");
            IsInit = true;
            _cycleCounter = 0;
            ServiceState = ServiceState.Initialized;
        }

        public void Start()
        {
            if (IsInit)
            {
                if (IsRunning)
                {
                    Log.Error("Agava io service already running");
                    return;
                }
                
                Log.Info("Starting IO Server.");
                _cycleCounter = 0;
                /*_cycleTimer = new Timer(CycleFunc, _modules,
                    _config.IOProcessorCycleTime,
                    _config.IOProcessorCycleTime);*/
                ServiceState = ServiceState.Running;
                IsRunning = true;
            }
        }

        public void Stop()
        {
            if (IsInit && IsRunning)
            {
                
                IsRunning = false;
                ServiceState = ServiceState.Stopped;
            }
        }

        public void Read()
        {
            foreach (var module in _modules.Values)
            {
               // var drequest = AgavaRequest.ReadInputRegisterRequest(module.ModuleId, 10000, 1);
                //var dresponse = _master.WriteRequest(drequest);

                var result = _rtuMaster.ReadInputRegisters(module.ModuleId, 10000, 1);
                module.SetDIRawData(result);

                if (_cycleCounter % _config.AnalogReadCycleDevider == 0)
                {
                    foreach (var iain in module.Pins.AnalogInputs.Values)
                    {
                        if (iain is AgavaAInput ain)
                        {
                            var reg = (ushort) (ain.PinNumberInModule * 2);

                            var aResult = _rtuMaster.ReadInputRegisters(module.ModuleId, reg, 2);

                            ain.SetRawValue(aResult);
                            /*var request = AgavaRequest.ReadInputRegisterRequest(module.ModuleId, reg, 2);
                            var response = _master.WriteRequest(request);
                            if (response.Data != null)
                            {
                                var value = response.Data[0]; //BufferToFloat(response.Data);
                                ain.SetRawValue(response.Data);
                            }*/
                            //Console.Write($"reg:{reg} pin:{ain.PinName} val:{BufferToFloat(response.Data)}");
                        }
                    }
                }
            }

            _cycleCounter++;
        }
        public void Write()
        {
            foreach (var module in _modules.Values)
            {
                /*var doRequest = AgavaRequest.WriteHoldingRegisterRequest(
                    module.ModuleId, 10000,new[]{module.DORegister});
                _master.WriteRequest(doRequest);*/
                _rtuMaster.WriteSingleRegisterAsync(module.ModuleId, 10000, module.DORegister);


                foreach (var ioutput in module.Pins.AnalogOutputs.Values)
                {
                    if (ioutput is AgavaAOutput output)
                    {
                        if (output.IsModified)
                        {
                            _rtuMaster.WriteMultipleRegistersAsync(module.ModuleId, output.RegAddress,
                                output.GetValue());
                            //var request = output.GetWriteValueRequest();
                            //_master.WriteRequest(request);
                        }
                    }
                }
            }
        }

        public Type ConfigType => typeof(ModbusConfig);
        public ServiceState ServiceState { get; private set; }

        public bool IsInit { get; private set; }


        #region Private functions

        private void EnueueRequest(AgavaRequest request)
        {
            if (request is null)
                return;
            lock (_queueLocker)
            {
                _requestQueue.Enqueue(request);
            }
        }


        
        private void ReadAnalogInputs()
        {
            
        }
        

        private bool CheckModule(byte moduleId)
        {
            var request = AgavaRequest.ReadHoldingRegisterRequest(moduleId, 2003, 1);
            var response = _master.WriteRequest(request);
            if (!response.ReplyTimeout)
                return true;
            else
                return false;
        }

        private void ScanBus(int startAddress, int endAddress)
        {
            if (startAddress < 1 || startAddress > 247)
                throw new ArgumentOutOfRangeException(nameof(startAddress),
                    "Адресс начала сканирования должен лежать в диапазоне от 1 до 247");
            if (endAddress < startAddress)
                throw new ArgumentOutOfRangeException(nameof(endAddress),
                    "Адресс окончания сканирования должен быть больше или равен адресу начала сканирования");

            for (var moduleId = (byte) startAddress; moduleId <= endAddress; moduleId++)
            {
                var request = AgavaRequest.ReadHoldingRegisterRequest(moduleId, 2017, 6);
                var response = _master.WriteRequest(request);
                if (!response.ReplyTimeout)
                {
                    var module = AgavaIOModule.CreateModule(moduleId, response.Data);
                    if (module != null)
                    {
                        if (_config.AnalogInputsTypes.Count > 0)
                            ConfigureModuleAnalog(module);
                        else
                            ReadModuleAnalogConfig(module);

                        _modules.Add(moduleId, module);
                    }
                }
                else
                {
                    Log.Debug($"Address:{moduleId} timeout");
                }
            }

            BuildPinsCollection();
        }

        private void ReadModuleAnalogConfig(AgavaIOModule module)
        {
            foreach (var iain in module.Pins.AnalogInputs.Values)
            {
                var ain = iain as AgavaAInput;
                if (ain is null)
                    continue;
                var regAddress = (ushort) (1200 + ain.PinNumberInModule);
                var request = AgavaRequest.ReadHoldingRegisterRequest(module.ModuleId, regAddress, 1);
                var response = _master.WriteRequest(request);

                Console.WriteLine($"Pin:{ain.PinName} type:{response.Data[0]}");

                var inType = (AgavaAnalogInType) response.Data[0];
                ain.InputType = inType;
                if (_config.AnalogInputsTypes.ContainsKey(ain.PinName))
                    _config.AnalogInputsTypes[ain.PinName] = inType;
                else
                    _config.AnalogInputsTypes.Add(ain.PinName, inType);
            }

            foreach (var iaout in module.Pins.AnalogOutputs.Values)
            {
                var aout = iaout as AgavaAOutput;
                if (aout is null)
                    continue;
                var regAddress = (ushort) (1400 + aout.PinNumberInModule);
                var request = AgavaRequest.ReadHoldingRegisterRequest(module.ModuleId, regAddress, 1);
                var response = _master.WriteRequest(request);

                var outType = (AgavaAnalogOutType) response.Data[0];
                aout.OutputType = outType;

                if (_config.AnalogOutputTypes.ContainsKey(aout.PinName))
                    _config.AnalogOutputTypes[aout.PinName] = outType;
                else
                    _config.AnalogOutputTypes.Add(aout.PinName, outType);
            }
            
        }

        private void ConfigureModuleAnalog(AgavaIOModule module)
        {
            foreach (var iain in module.Pins.AnalogInputs.Values)
            {
                var ain = iain as AgavaAInput;
                if (ain is null)
                    continue;

                var regAddress = (ushort) (1200 + ain.PinNumberInModule);

                var inputType = (ushort) _config.AnalogInputsTypes[ain.PinName];

                var request = AgavaRequest.WriteHoldingRegisterRequest(
                    module.ModuleId, regAddress, new ushort[] {inputType});
                _master.WriteRequest(request);

                switch (_config.AnalogInputsTypes[ain.PinName])
                {
                    case AgavaAnalogInType.Voltage_0_10V:
                        ain.ValueConverter = new VoltageToPercentConverter();
                        break;
                    case AgavaAnalogInType.Current_4_20mA:
                        break;
                    case AgavaAnalogInType.Current_0_20mA:
                        break;
                    case AgavaAnalogInType.Current_0_5mA:
                        break;
                    case AgavaAnalogInType.Resistance:
                        ain.ValueConverter = new Pt1000ToTemperature();
                        break;
                    case AgavaAnalogInType.TR_Pt100:
                        ain.ValueConverter = new Pt1000ToTemperature();
                        break;
                    case AgavaAnalogInType.TR_Pt1000:
                        ain.ValueConverter = new Pt1000ToTemperature();
                        break;
                    case AgavaAnalogInType.TR_50M:
                        break;
                    case AgavaAnalogInType.TR_100M:
                        break;
                    case AgavaAnalogInType.TC_L:
                        break;
                    case AgavaAnalogInType.TC_J:
                        break;
                    case AgavaAnalogInType.TC_N:
                        break;
                    case AgavaAnalogInType.TC_K:
                        break;
                    case AgavaAnalogInType.TC_S:
                        break;
                    case AgavaAnalogInType.TC_R:
                        break;
                    case AgavaAnalogInType.TC_B:
                        break;
                    case AgavaAnalogInType.TC_A1:
                        break;
                    case AgavaAnalogInType.TC_A2:
                        break;
                    case AgavaAnalogInType.TC_A3:
                        break;
                    case AgavaAnalogInType.TR_TSP50:
                        break;
                    case AgavaAnalogInType.TR_TSP100:
                        break;
                    case AgavaAnalogInType.Millivolts:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            foreach (var ioutput in module.Pins.AnalogOutputs.Values)
            {
                var output = ioutput as AgavaAOutput;
                if (output is null)
                    continue;

                var regAddress = (ushort) (1400 + output.PinNumberInModule);

                var outputType = (ushort) _config.AnalogOutputTypes[output.PinName];

                var request = AgavaRequest.WriteHoldingRegisterRequest(
                    module.ModuleId, regAddress, new ushort[] {outputType});
                _master.WriteRequest(request);
            }
        }

        private SerialPort CreatePort()
        {
            var port = new SerialPort();
            port.PortName = _config.PortName;
            port.BaudRate = _config.BaudRate;

            Console.WriteLine($"Starting Modbus server on port:{_config.PortName}");

            try
            {
                port.Open();
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

            return port;
        }

        private void BuildPinsCollection()
        {
            foreach (var module in _modules.Values)
            {
                foreach (var ain in module.Pins.AnalogInputs.Values) _pins.AnalogInputs.Add(ain.PinName, ain);

                foreach (var aout in module.Pins.AnalogOutputs.Values) _pins.AnalogOutputs.Add(aout.PinName, aout);

                foreach (var din in module.Pins.DiscreteInputs.Values) _pins.DiscreteInputs.Add(din.PinName, din);

                foreach (var dout in module.Pins.DiscreteOutputs.Values) _pins.DiscreteOutputs.Add(dout.PinName, dout);
            }
        }

        #endregion Private functions
    }
}