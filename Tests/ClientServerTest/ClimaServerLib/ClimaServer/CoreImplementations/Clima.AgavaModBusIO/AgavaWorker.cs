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
        private object queueLocker = null;
        private Queue<AgavaRequest> _requestQueue;
        private Thread _workerThread;
        
        private bool _isRunning;
        private bool _exitSignal;

        #endregion Private fields
        #region Events

        public event ReplyReceivedEventHandler ReplyReceived;
        protected virtual void OnReplyReceived(AgavaReply reply)
        {
            ReplyReceived?.Invoke(reply);
        }

        #endregion Events
        #region Public Methods
        
        public AgavaWorker(Dictionary<byte, AgavaIOModule> modules, IModbusSerialMaster master)
        {
            _modules = modules;
            
            _master = master;
            _requestQueue = new Queue<AgavaRequest>();
            _workerThread = new Thread(Run);
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
            
        }

        public void StopWorker()
        {
            
        }
        
        public void EnqueueRequest(AgavaRequest request)
        {
            lock (queueLocker)
            {
                _requestQueue.Enqueue(request);
            }
        }
        
        #endregion Public Methods

        #region Public properties

        public int CycleTime { get; set; }
        public int DiscreteCycleDevider { get; set; }
        public int AnalogCycleDevider { get; set; }

        #endregion Public properties
        #region Private mothods
        private void Run()
        {
            Console.WriteLine("IO Thread started.");
            
            while (!_exitSignal)
            {
                ProcessDiscreteOutputs();
                if (_requestQueue.Count > 0)
                {
                    ProcessRequest(_requestQueue.Dequeue());
                }
                
                Thread.Sleep(CycleTime);
            }
            Console.WriteLine("IO Thread exited.");
        }
        
        
        private void ProcessRequest(AgavaRequest request)
        {
            AgavaReply reply = new AgavaReply();
            
            switch (request.RequestType)
            {
                case RequestType.ReadCoils:
                    reply.Data = _master.ReadCoils(
                        request.ModuleID,
                        request.RegisterAddress,
                        request.DataCount)
                        .Select(b => (object) b).ToArray();
                    break;
                case RequestType.ReadDiscreteInputs:
                    reply.Data = _master.ReadInputs(
                            request.ModuleID,
                            request.RegisterAddress,
                            request.DataCount)
                        .Select(b => (object) b).ToArray();
                    break;
                case RequestType.ReadHoldingRegisters:
                    reply.Data = _master.ReadHoldingRegisters(
                            request.ModuleID,
                            request.RegisterAddress,
                            request.DataCount)
                        .Select(b => (object) b).ToArray();
                    break;
                case RequestType.ReadInputRegisters:
                    reply.Data = _master.ReadInputRegisters(
                            request.ModuleID,
                            request.RegisterAddress,
                            request.DataCount)
                        .Select(b => (object) b).ToArray();
                    break;
                case RequestType.WriteSingleCoil:
                    _master.WriteSingleCoil(request.ModuleID,request.RegisterAddress,(bool)request.Data[0]);
                    break;
                case RequestType.WriteSingleRegister:
                    _master.WriteSingleRegister(request.ModuleID,request.RegisterAddress,(ushort)request.Data[0]);
                    break;
                case RequestType.WriteMultipleCoils:
                    _master.WriteMultipleCoils(request.ModuleID, request.RegisterAddress, request.Data.Select(b=>(bool)b).ToArray());
                    break;
                case RequestType.WriteMultipleRegisters:
                    _master.WriteMultipleRegisters(request.ModuleID, request.RegisterAddress, request.Data.Select(b=>(ushort)b).ToArray());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            reply.ModuleID = request.ModuleID;
            reply.RegisterAddress = request.RegisterAddress;
            reply.RequestType = request.RequestType;
            
            OnReplyReceived(reply);
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
        #endregion Private mothods
    }
}