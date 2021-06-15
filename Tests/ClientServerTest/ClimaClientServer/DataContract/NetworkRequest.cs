namespace DataContract
{
    public enum RequestType : byte
    {
        GetInfo = 1,
        Authorize = 2,
        CreateSession = 3
    }
    public class NetworkRequest
    {
        public NetworkRequest()
        {
            RequestType = RequestType.GetInfo;
        }
        public RequestType RequestType { get; }
        public byte[] Data;
    }
}