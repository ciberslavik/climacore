﻿using System;
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

            reply.ModuleID = request.ModuleID;
            reply.RegisterAddress = request.RegisterAddress;
            reply.RequestType = request.RequestType;
            
            OnReplyReceived(reply);
        }
        
        
    }
}