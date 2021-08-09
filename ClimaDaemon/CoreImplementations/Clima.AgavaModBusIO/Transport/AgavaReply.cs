namespace Clima.AgavaModBusIO.Transport
{
    public class AgavaReply
    {
        public AgavaReply()
        {
        }

        public AgavaReply(AgavaRequest request)
        {
            ModuleID = request.ModuleID;
            RegisterAddress = request.RegisterAddress;
            RequestType = request.RequestType;
            DataCount = request.DataCount;
        }

        public RequestType RequestType { get; set; }
        public byte ModuleID { get; set; }
        public ushort RegisterAddress { get; set; }
        public ushort DataCount { get; set; }
        public ushort[] Data { get; set; }
        public bool[] Coils { get; set; }
        public bool ReplyTimeout { get; set; }
    }
}