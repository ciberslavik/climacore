namespace Clima.AgavaModBusIO
{
    public class AgavaReply
    {
        public AgavaReply()
        {
        }
        public RequestType RequestType { get; set; }
        public int ModuleID { get; set; }
        public int RegisterAddress { get; set; }
        public object[] Data { get; set; }
    }
}