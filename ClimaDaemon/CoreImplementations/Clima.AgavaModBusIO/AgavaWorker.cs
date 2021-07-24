using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Clima.AgavaModBusIO.Model;
using Clima.Core.IO;
using NModbus;

namespace Clima.AgavaModBusIO
{
    public delegate void ReplyReceivedEventHandler(AgavaReply reply);
    public class AgavaWorker
    {
        #region Private fields

        private readonly Dictionary<byte, AgavaIOModule> _modules;
        private readonly IModbusSerialMaster _master;
        private static object _queueLocker = new object();
        private static Queue<AgavaRequest> _requestQueue;
        private Timer _cycleTimer;
        private long _cycleCounter;
        private bool _isRunning;
        private bool _exitSignal;

        #endregion Private fields
        #region Events

        public event ReplyReceivedEventHandler ReplyReceived;
        protected virtual void OnReplyReceived(AgavaReply reply)
        {
            switch (reply.RequestType)
            {
                case RequestType.ReadCoils:
                    break;
                case RequestType.ReadDiscreteInputs:
                    break;
                case RequestType.ReadHoldingRegisters:
                    if (reply.RegisterAddress == 2003)
                    {
                        var module = _modules[reply.ModuleID];
                        
                    }
                    break;
                case RequestType.ReadInputRegisters:
                    if (reply.RegisterAddress == 10000)
                    {
                        if (_modules.ContainsKey(reply.ModuleID))
                        {
                            _modules[reply.ModuleID].SetDIRawData(
                                reply.Data.Select(s => (ushort) s).ToArray());
                        }
                    }
                    else if (CheckAnalogInAddres(reply.ModuleID,reply.RegisterAddress) )
                    {
                        PrintData(reply.Data);
                        
                    }
                    break;
                case RequestType.WriteSingleCoil:
                    break;
                case RequestType.WriteSingleRegister:
                    break;
                case RequestType.WriteMultipleCoils:
                    break;
                case RequestType.WriteMultipleRegisters:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion Events
        #region Public Methods
        
        public AgavaWorker(Dictionary<byte, AgavaIOModule> modules, IModbusSerialMaster master)
        {
            _modules = modules;
            _master = master;
            _requestQueue = new Queue<AgavaRequest>();
            _cycleCounter = 0;
        }

        private void OnAnalogOutputChanged(AnalogPinValueChangedEventArgs ea)
        {
            throw new NotImplementedException();
        }

        private void OnDiscreteOutputChanged(DiscretePinStateChangedEventArgs ea)
        {
            throw new NotImplementedException();
        }

        public void StartWorker()
        {
            if(_isRunning)
                return;

            _isRunning = true;
            _cycleCounter = 0;
            _cycleTimer = new Timer(TimerCallback, null, 100, CycleTime);
            Console.WriteLine("AGAVA IO Worker started");
        }

        public void StopWorker()
        {
            _exitSignal = true;
            _isRunning = false;
            _cycleTimer.Dispose();
        }
        
        public void EnqueueRequest(AgavaRequest request)
        {
            if(request==null)
                return;
            
            lock (_queueLocker)
            {
                _requestQueue.Enqueue(request);
            }
        }
        
        #endregion Public Methods

        #region Public properties

        public int CycleTime { get; set; }
        public int DiscreteCycleDevider { get; set; } = 1;
        public int AnalogCycleDevider { get; set; } = 10;

        public bool IsRunning => _isRunning;
        #endregion Public properties
        #region Private mothods

        private bool CheckAnalogInAddres(byte moduleId, ushort address)
        {
            var maxAddr = _modules[moduleId].Pins.AnalogInputs.Count * 2;
            if (address >= 0 && address <= maxAddr)
            {
                return true;
            }

            return false;
        }
        private void TimerCallback(object o)
        {
            ProcessDiscreteOutputs();
            ProcessAnalogOutputs();
            lock (_queueLocker)
            {
                if (_requestQueue.Count > 0)
                {
                    ProcessRequest(_requestQueue.Dequeue());
                }    
            }
            
            _cycleCounter++;
            
            if (_cycleCounter % DiscreteCycleDevider == 0)
            {
                foreach (var module in _modules.Values)
                {
                    AgavaRequest readRequest;
                    if (module.Pins.DiscreteInputs.Count > 0)
                    {
                        readRequest = GetReadDIRequest(module.ModuleId);
                    }
                    else
                    {
                        readRequest = GetModuleStatusRequest(module.ModuleId);
                    }
                    EnqueueRequest(readRequest);   
                }
                
            }

            if (_cycleCounter % AnalogCycleDevider == 0)
            {
                foreach (var module in _modules.Values)
                {
                    if (module.Pins.AnalogInputs.Count > 0)
                    {
                        foreach (var inputPin in module.Pins.AnalogInputs.Values)
                        {
                            if (inputPin is AgavaAInput pin)
                            {
                                var readAnalogRequest = new AgavaRequest();
                                readAnalogRequest.ModuleID = pin.ModuleId;
                                readAnalogRequest.RegisterAddress = pin.RegAddress;
                                readAnalogRequest.DataCount = 2;
                                readAnalogRequest.RequestType = RequestType.ReadInputRegisters;
                                EnqueueRequest(readAnalogRequest);
                            }
                            
                        }
                    }
                }
            }
        }

        private AgavaRequest GetModuleStatusRequest(byte moduleId)
        {
            if (_modules.ContainsKey(moduleId))
            {
                var request = new AgavaRequest();
                request.ModuleID = moduleId;
                request.RequestType = RequestType.ReadHoldingRegisters;
                request.DataCount = 2;
                request.RegisterAddress = 2003;
                
                return request;
            }

            return null;
        }
        private AgavaRequest GetReadDIRequest(byte moduleId)
        {
            if (_modules.ContainsKey(moduleId))
            {
                if (_modules[moduleId].Pins.DiscreteInputs.Count > 0)
                {
                    var request = new AgavaRequest();
                    var module = _modules[moduleId];
                    request.ModuleID = module.ModuleId;
                    request.RegisterAddress = 10000;
                    request.RequestType = RequestType.ReadInputRegisters;
                    request.DataCount = 2;
                    return request;
                }
            }

            return null;
        }

        private void ProcessRequest(AgavaRequest request)
        {
            request.Print();
            AgavaReply reply = new AgavaReply();
            bool isRead = false;
            switch (request.RequestType)
            {
                case RequestType.ReadCoils:
                    reply.Data = _master.ReadCoils(
                        request.ModuleID,
                        request.RegisterAddress,
                        request.DataCount)
                        .Select(b => (object) b).ToArray();
                    isRead = true;
                    break;
                case RequestType.ReadDiscreteInputs:
                    reply.Data = _master.ReadInputs(
                            request.ModuleID,
                            request.RegisterAddress,
                            request.DataCount)
                        .Select(b => (object) b).ToArray();
                    isRead = true;
                    break;
                case RequestType.ReadHoldingRegisters:
                    reply.Data = _master.ReadHoldingRegisters(
                            request.ModuleID,
                            request.RegisterAddress,
                            request.DataCount)
                        .Select(b => (object) b).ToArray();
                    isRead = true;
                    break;
                case RequestType.ReadInputRegisters:
                    reply.Data = _master.ReadInputRegisters(
                            request.ModuleID,
                            request.RegisterAddress,
                            request.DataCount)
                        .Select(b => (object) b).ToArray();
                    isRead = true;
                    break;
                case RequestType.WriteSingleCoil:
                    _master.WriteSingleCoil(request.ModuleID,request.RegisterAddress,(bool)request.Data[0]);
                    isRead = false;
                    break;
                case RequestType.WriteSingleRegister:
                    _master.WriteSingleRegister(request.ModuleID,request.RegisterAddress,(ushort)request.Data[0]);
                    isRead = false;
                    break;
                case RequestType.WriteMultipleCoils:
                    _master.WriteMultipleCoils(request.ModuleID, request.RegisterAddress, request.Data.Select(b=>(bool)b).ToArray());
                    isRead = false;
                    break;
                case RequestType.WriteMultipleRegisters:
                    _master.WriteMultipleRegisters(request.ModuleID, request.RegisterAddress, request.Data.Select(b=>(ushort)b).ToArray());
                    isRead = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (isRead)
            {
                reply.ModuleID = request.ModuleID;
                reply.RegisterAddress = request.RegisterAddress;
                reply.RequestType = request.RequestType;
                
                OnReplyReceived(reply);
            }
        }

        private void ProcessDiscreteOutputs()
        {
            foreach (var module in _modules.Values.ToList())
            {
                if (module.IsDiscreteModified)
                {
                    var doRegister = module.GetDORawData();
                    AgavaRequest request = new AgavaRequest();
                    request.ModuleID = module.ModuleId;
                    request.RegisterAddress = 10000;
                    request.DataCount = (ushort)doRegister.Length;
                    request.Data = doRegister.Select(b => (object) b).ToArray();
                    
                    EnqueueRequest(request);
                }
            }
        }
        
        private void ProcessAnalogOutputs()
        {
            foreach (var module in _modules.Values.ToList())
            {
                if (module.IsAnalogModified)
                {
                    
                }
            }
        }

        private void PrintData(object[] Data)
        {
            ushort[] data = Data.Select(d => (ushort) d).ToArray();
            string printStr = "Data:";
            foreach (var d in data)
            {
                printStr += $"{d:X}, ";
            }
            
            Console.WriteLine(printStr);
        }
        #endregion Private mothods
    }
}