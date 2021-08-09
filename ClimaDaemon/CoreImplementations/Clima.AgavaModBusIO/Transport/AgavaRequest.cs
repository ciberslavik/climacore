using System;

namespace Clima.AgavaModBusIO.Transport
{
    public enum RequestType : byte
    {
        ReadCoils = 0x01,
        ReadHoldingRegisters = 0x03,
        ReadInputRegisters = 0x04,
        WriteSingleCoil = 0x05,
        WriteSingleRegister = 0x06,
        WriteMultipleCoils = 0x0F,
        WriteMultipleRegisters = 0x10
    }

    public class AgavaRequest
    {
        public AgavaRequest()
        {
        }

        public RequestType RequestType { get; set; }
        public byte ModuleID { get; set; }
        public ushort RegisterAddress { get; set; }
        public ushort DataCount { get; set; }
        public ushort[] Data { get; set; }

        public void Print()
        {
            Console.WriteLine($"Request:\r\n" +
                              $"  Module:{ModuleID}\r\n" +
                              $"  ReagAddress:{RegisterAddress}\r\n" +
                              $"  RequestType:{RequestType}" +
                              $"  DataCount:{DataCount}");
        }

        private static AgavaRequest CreateRequest(byte moduleId, ushort regAddress, ushort dataCount,
            RequestType requestType, ushort[] data)
        {
            var request = new AgavaRequest();
            request.ModuleID = moduleId;
            request.RegisterAddress = regAddress;
            request.DataCount = dataCount;
            request.RequestType = requestType;
            request.Data = data;
            return request;
        }

        public static AgavaRequest ReadInputRegisterRequest(byte moduleId, ushort regAddress, ushort dataCount)
        {
            var request = CreateRequest(moduleId, regAddress, dataCount,
                RequestType.ReadInputRegisters, null);
            request.RequestType = RequestType.ReadInputRegisters;

            return request;
        }

        public static AgavaRequest ReadHoldingRegisterRequest(byte moduleId, ushort regAddress, ushort dataCount)
        {
            var request = CreateRequest(moduleId, regAddress, dataCount,
                RequestType.ReadInputRegisters, null);
            request.RequestType = RequestType.ReadHoldingRegisters;

            return request;
        }

        public static AgavaRequest WriteHoldingRegisterRequest(byte moduleId, ushort regAddress, ushort[] data)
        {
            var request = CreateRequest(moduleId, regAddress, 1,
                RequestType.WriteMultipleRegisters, data);
            return request;
        }
    }
}