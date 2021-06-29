using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Clima.Services.IO;
using NModbus;

namespace Clima.AgavaModBusIO
{
    public delegate void ReplyReceivedEventHandler(AgavaReply reply);
    public class AgavaWorker
    {
        private readonly IOPinCollection _pins;
        private readonly IModbusSerialMaster _master;
        private object queueLocker;
        private Queue<AgavaRequest> _requestQueue;
        private Thread _workerThread;

        private bool _isRunning;
        private bool _exitSignal;

        #region Events

        public event ReplyReceivedEventHandler ReplyReceived;
        protected virtual void OnReplyReceived(AgavaReply reply)
        {
            ReplyReceived?.Invoke(reply);
        }

        #endregion
        #region Public Methods
        
        public AgavaWorker(IOPinCollection pins, IModbusSerialMaster master)
        {
            _pins = pins;
            _master = master;
            _requestQueue = new Queue<AgavaRequest>();
            _workerThread = new Thread(Run);
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
        private void Run()
        {
            while (!_exitSignal)
            {
                
            }
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
        
        
    }
}