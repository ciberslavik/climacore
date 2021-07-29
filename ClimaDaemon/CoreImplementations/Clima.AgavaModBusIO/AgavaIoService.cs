using System.Collections.Generic;
using System;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using Clima.AgavaModBusIO.Configuration;
using Clima.AgavaModBusIO.Model;
using Clima.AgavaModBusIO.Transport;
using Clima.Basics.Configuration;
using Clima.Basics.Services;
using Clima.Core.IO;
using Clima.Core.IO.Converters;
using NModbus;
using NModbus.Serial;


namespace Clima.AgavaModBusIO
{
    public class AgavaIoService:IIOService
    {
        private readonly IConfigurationStorage _configStorage;
        private ModbusConfig _config;
        private IAgavaMaster _master;
        private IOPinCollection _pins;
        private Dictionary<byte, AgavaIOModule> _modules;
        private Queue<AgavaRequest> _requestQueue;
        private static object _queueLocker = new object();
        private Timer _cycleTimer;
        private long _cycleCounter;        
        public AgavaIoService(IConfigurationStorage configStorage)
        {
            _configStorage = configStorage;
            _pins = new IOPinCollection();
            _modules = new Dictionary<byte, AgavaIOModule>();
            _requestQueue = new Queue<AgavaRequest>();
        }

        public bool IsRunning { get; private set; }
        public ISystemLogger Log { get; set; }
        public IOPinCollection Pins => _pins;


        public void Init()
        {
            if (!_configStorage.Exist("ModbusConfig"))
            {
                var defaultConfig = ModbusConfig.CreateDefault();
                _configStorage.RegisterConfig("ModbusConfig", defaultConfig);
            }
            
            _config = _configStorage.GetConfig<ModbusConfig>("ModbusConfig");
            
            var port = CreatePort();

            _master = new AgavaModbusRTUMaster(port);
            
            _master.ReadTimeout = _config.ResponseTimeout;
            _master.WriteTimeout = _config.ResponseTimeout;
            
            Console.WriteLine("Start scanning IO bus.");
            Log.Debug("Start scanning IO bus.");
            
            ScanBus(_config.BusStartAddress, _config.BusEndAddress);
            
            Log.Debug("Read analogInputs");
            ReadAnalogInputs();
            
            Console.WriteLine($"IO System  initialization complete Thread:{Thread.CurrentThread.ManagedThreadId}");
            IsInit = true;
        }

        public void Start()
        {
            if (IsInit)
            {
                Console.WriteLine("Starting IO Server.");
                _cycleCounter = 0;
                _cycleTimer = new Timer(CycleFunc, _modules, 100, _config.IOProcessorCycleTime);
            }
        }
        public void Stop()
        {
            
        }

        public bool IsInit { get; private set; }
        
        
        #region Private functions

        private void EnueueRequest(AgavaRequest request)
        {
            if(request is null)
                return;
            lock (_queueLocker)
            {
                _requestQueue.Enqueue(request);
            }
        }

        
        private void CycleFunc(object state)
        {
            var modules = state as Dictionary<byte, AgavaIOModule>;
            if (modules is null)
                return;
            
            _cycleCounter++;
            
            foreach (var moduleId in modules.Keys)
            {
                //Read modules status for check is module live!
                if (CheckModule(moduleId))
                {
                    var module = modules[moduleId];
                    if (module.IsDiscreteModified)
                    {
                        var request = AgavaRequest.WriteHoldingRegisterRequest(
                                moduleId, 10000, module.GetDORawData());
                        _master.WriteRequest(request);
                        module.AcceptModify();
                    }

                    if (module.IsAnalogModified)
                    {
                        foreach (var ioutput in module.Pins.AnalogOutputs.Values)
                        {
                            var output = ioutput as AgavaAOutput;
                            if(output is null)
                                continue;
                            if (output.IsModified)
                            {
                                var request = output.GetWriteValueRequest();
                                _master.WriteRequest(request);
                            }
                        }
                    }

                    if (_cycleCounter % _config.DiscreteReadCycleDevider == 0)
                    {
                        var request = AgavaRequest.ReadInputRegisterRequest(moduleId, 10000, 1);
                        var response = _master.WriteRequest(request);
                        modules[moduleId].SetDIRawData(response.Data);
                    }

                    if (_cycleCounter % _config.AnalogReadCycleDevider == 0)
                    {
                        foreach (var iain in modules[moduleId].Pins.AnalogInputs.Values)
                        {
                            var ain = iain as AgavaAInput;
                            if(ain is null)
                                continue;

                            var request = AgavaRequest.ReadInputRegisterRequest(moduleId, ain.RegAddress, 2);
                            var response = _master.WriteRequest(request);
                            Console.Write($"Pin:{ain.PinName} ");
                            var value = BufferToFloat(response.Data);
                            Console.WriteLine($" value:{value:F10}");
                            ain.SetRawValue(value);
                        }
                    }
                }
            }
        }

        private void ReadAnalogInputs()
        {
            foreach (var module in _modules.Values)
            {
                foreach (var iain in module.Pins.AnalogInputs.Values)
                {
                    if (iain is AgavaAInput ain)
                    {
                        var request = AgavaRequest.ReadInputRegisterRequest(module.ModuleId, ain.RegAddress, 1);
                        var response = _master.WriteRequest(request);
                            
                        var value = response.Data[0];//BufferToFloat(response.Data);
                        ain.SetRawValue(value);
                    }
                }
            }
        }
        private float BufferToFloat(ushort[] buffer)
        {
            byte[] bytes = new byte[4];
            byte[] b1 = BitConverter.GetBytes(buffer[0]);
            byte[] b2 = BitConverter.GetBytes(buffer[1]);
            Console.Write($" buffer:{buffer[0]:X}, {buffer[1]:X}");
            bytes[0] = b1[0];
            bytes[1] = b1[1];
            bytes[2] = b2[0];
            bytes[3] = b2[1];

            return BufferToFloat2(bytes);
            /*Pin:AI:1:0  buffer:FF40, 0 value:0,0000000000
            Pin:AI:1:1  buffer:FF0B, 0 value:0,0000000000
            Pin:AI:1:2  buffer:1, 0 value:0,0000000000
            Pin:AI:1:3  buffer:0, 0 value:0,0000000000
            Pin:AI:1:4  buffer:0, 0 value:0,0000000000
            Pin:AI:1:5  buffer:2, 0 value:0,0000000000
            Pin:AI:1:6  buffer:18, 0 value:0,0000000000
            Pin:AI:1:7  buffer:40, 0 value:0,0000000000
            Pin:AI:1:8  buffer:0, 0 value:0,0000000000
            Pin:AI:1:9  buffer:0, 0 value:0,0000000000*/

        }

        private float BufferToFloat2(byte[] data)
        {
            uint fb = BitConverter.ToUInt32(data);

            int sign = (int)((fb >> 31) & 1);
            int exponent = (int)((fb >> 23) & 0xFF);
            int mantissa = (int)(fb & 0x7FFFFF);

            float fMantissa;
            float fSign = sign == 0 ? 1.0f : -1.0f;

            if (exponent != 0)
            {
                exponent -= 127;
                fMantissa = 1.0f + (mantissa / (float)0x800000);
            }
            else
            {
                if (mantissa != 0)
                {
                    // denormal
                    exponent -= 126;
                    fMantissa = 1.0f / (float)0x800000;
                }
                else
                {
                    // +0 and -0 cases
                    fMantissa = 0;
                }
            }

            float fExponent = (float)Math.Pow(2.0, exponent);
            float ret = fSign * fMantissa * fExponent;
            return ret;
        }
        private bool CheckModule(byte moduleId)
        {
            var request = AgavaRequest.ReadHoldingRegisterRequest(moduleId, 2003, 1);
            var response = _master.WriteRequest(request);
            if (!response.ReplyTimeout)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void ScanBus(int startAddress, int endAddress)
        {
            if ((startAddress < 1 || startAddress > 247))
                throw new ArgumentOutOfRangeException(nameof(startAddress), "Адресс начала сканирования должен лежать в диапазоне от 1 до 247");
            if (endAddress < startAddress)
                throw new ArgumentOutOfRangeException(nameof(endAddress),"Адресс окончания сканирования должен быть больше или равен адресу начала сканирования");
            
            for (byte moduleId = (byte)startAddress; moduleId <= endAddress; moduleId++)
            {
                var request = AgavaRequest.ReadHoldingRegisterRequest(moduleId, 2017, 6);
                var response = _master.WriteRequest(request);
                if (!response.ReplyTimeout)
                {
                    var module = AgavaIOModule.CreateModule(moduleId, response.Data);
                    if (module != null)
                    {
                        
                        if(_config.AnalogInputsTypes.Count>0)
                            ConfigureModuleAnalog(module);
                        else
                            ReadModuleAnalogConfig(module);
                        
                        _modules.Add(moduleId, module);
                    }
                }
            }
            _configStorage.Save();
            BuildPinsCollection();
        }

        private void ReadModuleAnalogConfig(AgavaIOModule module)
        {
            foreach (var iain in module.Pins.AnalogInputs.Values)
            {
                var ain = iain as AgavaAInput;
                if (ain is null)
                    continue;
                var regAddress = (ushort)(1200 + ain.PinNumberInModule);
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
                if(aout is null)
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
                if(ain is null)
                    continue;

                var regAddress = (ushort)(1200 + ain.PinNumberInModule);

                ushort inputType = (ushort) _config.AnalogInputsTypes[ain.PinName];

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
                        break;
                    case AgavaAnalogInType.TR_Pt100:
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
                if(output is null)
                    continue;

                var regAddress = (ushort)(1400 + output.PinNumberInModule);

                ushort outputType = (ushort) _config.AnalogOutputTypes[output.PinName];

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
        #endregion Private functions
    }
}
