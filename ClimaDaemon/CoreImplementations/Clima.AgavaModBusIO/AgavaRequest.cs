using System;

namespace Clima.AgavaModBusIO
{
    public enum RequestType : byte
    {
        ReadCoils = 0x01,
        ReadDiscreteInputs = 0x02,
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
        public object[] Data { get; set; }

        public void Print()
        {
            Console.WriteLine($"Request:\r\n" +
                              $"  Module:{ModuleID}\r\n" +
                              $"  ReagAddress:{RegisterAddress}\r\n" +
                              $"  RequestType:{RequestType}" +
                              $"  DataCount:{DataCount}");
        }
    }
}