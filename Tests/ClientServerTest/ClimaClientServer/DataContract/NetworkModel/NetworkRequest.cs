namespace DataContract.NetworkModel
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
            RequestType = "GetInfo";
        }

        public string RequestType;
        public string Data;
    }
}