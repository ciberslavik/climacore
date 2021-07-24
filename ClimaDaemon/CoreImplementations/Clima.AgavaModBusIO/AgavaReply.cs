namespace Clima.AgavaModBusIO
{
    public class AgavaReply
    {
        public AgavaReply()
        {
        }
        public RequestType RequestType { get; set; }
        public byte ModuleID { get; set; }
        public ushort RegisterAddress { get; set; }
        public ushort DataCount { get; set; }
        public object[] Data { get; set; }
    }
}