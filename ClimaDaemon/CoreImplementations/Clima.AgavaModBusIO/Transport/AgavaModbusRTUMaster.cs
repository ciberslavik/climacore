using System;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using NModbus;
using NModbus.Serial;

namespace Clima.AgavaModBusIO.Transport
{
    public class AgavaModbusRTUMaster:IAgavaMaster
    {
        private IModbusMaster _master;
        public SerialPort Port { get; }

        public AgavaModbusRTUMaster(SerialPort port)
        {
            Port = port;
            
            var factory = new ModbusFactory();
            var transport = factory.CreateRtuTransport(Port);
            _master = factory.CreateMaster(transport);
            
        }

        public async void WriteRequest(AgavaRequest request)
        {

            bool isRead = false;
            Task ioTask;
            switch (request.RequestType)
            {
                case RequestType.ReadCoils:
                    ioTask = new Task(() =>
                    {
                        AgavaReply reply = new AgavaReply(request);
                        try
                        {
                            reply.Coils = _master.ReadCoils(request.ModuleID, request.RegisterAddress,
                                request.DataCount);
                            reply.DataCount = (ushort) reply.Coils.Length;
                            reply.ReplyTimeout = false;
                        }
                        catch (TimeoutException e)
                        {
                            reply.ReplyTimeout = true;
                        }

                        OnReplyReceived(new ReplyReceivedEventArgs(reply));
                    });
                    break;
                case RequestType.ReadHoldingRegisters:
                    ioTask = new Task(() =>
                    {
                        AgavaReply reply = new AgavaReply(request);
                        try
                        {
                            reply.Data = _master.ReadHoldingRegisters(request.ModuleID, request.RegisterAddress,
                                request.DataCount);
                            reply.DataCount = (ushort) reply.Data.Length;
                            reply.ReplyTimeout = false;
                        }
                        catch (TimeoutException e)
                        {
                            reply.ReplyTimeout = true;
                        }

                        OnReplyReceived(new ReplyReceivedEventArgs(reply));
                    });
                    break;
                case RequestType.ReadInputRegisters:
                    ioTask = new Task(() =>
                    {
                        AgavaReply reply = new AgavaReply(request);
                        try
                        {
                            reply.Data = _master.ReadInputRegisters(request.ModuleID, request.RegisterAddress,
                                request.DataCount);
                            reply.DataCount = (ushort) reply.Data.Length;
                            reply.ReplyTimeout = false;
                        }
                        catch (TimeoutException e)
                        {
                            reply.ReplyTimeout = true;
                        }

                        OnReplyReceived(new ReplyReceivedEventArgs(reply));
                    });
                    break;
                case RequestType.WriteSingleCoil:
                    ioTask = new Task(() =>
                    {
                        AgavaReply reply = new AgavaReply(request);
                        try
                        {
                            _master.WriteSingleCoil(request.ModuleID, request.RegisterAddress, request.Data[0] != 0);
                            reply.ReplyTimeout = false;
                        }
                        catch (TimeoutException e)
                        {
                            reply.ReplyTimeout = true;
                        }

                        OnReplyReceived(new ReplyReceivedEventArgs(reply));
                    });
                    break;
                case RequestType.WriteSingleRegister:
                    ioTask = new Task(() =>
                    {
                        AgavaReply reply = new AgavaReply(request);
                        try
                        {
                            _master.WriteSingleRegister(request.ModuleID, request.RegisterAddress, request.Data[0]);
                            reply.ReplyTimeout = false;
                        }
                        catch (TimeoutException e)
                        {
                            reply.ReplyTimeout = true;
                        }

                        OnReplyReceived(new ReplyReceivedEventArgs(reply));
                    });
                    break;
                case RequestType.WriteMultipleCoils:
                    ioTask = new Task(() =>
                    {
                        AgavaReply reply = new AgavaReply(request);
                        try
                        {
                            _master.WriteMultipleCoils(request.ModuleID, request.RegisterAddress,
                                request.Data.Select(d => d != 0).ToArray());
                            reply.ReplyTimeout = false;
                        }
                        catch (TimeoutException e)
                        {
                            reply.ReplyTimeout = true;
                        }

                        OnReplyReceived(new ReplyReceivedEventArgs(reply));
                    });
                    break;
                case RequestType.WriteMultipleRegisters:
                    ioTask = new Task(() =>
                    {
                        AgavaReply reply = new AgavaReply(request);
                        try
                        {
                            _master.WriteMultipleRegisters(request.ModuleID, request.RegisterAddress, request.Data);
                            reply.ReplyTimeout = false;
                        }
                        catch (TimeoutException e)
                        {
                            reply.ReplyTimeout = true;
                        }

                        OnReplyReceived(new ReplyReceivedEventArgs(reply));
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (ioTask != null)
            { 
                ioTask.Start();
            }
        }

        public event ReplyReceivedEventHandler ReplyReceived;
        public int ReadTimeout { get=>_master.Transport.ReadTimeout;
            set => _master.Transport.ReadTimeout = value;
        }
        public int WriteTimeout { get=>_master.Transport.WriteTimeout;
            set => _master.Transport.WriteTimeout = value;
        }

        protected virtual void OnReplyReceived(ReplyReceivedEventArgs ea)
        {
            ReplyReceived?.Invoke(this, ea);
        }
    }
}