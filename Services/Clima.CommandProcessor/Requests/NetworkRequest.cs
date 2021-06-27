namespace Clima.CommandProcessor.Requests
{
    public class NetworkRequest
    {
        public NetworkRequest()
        {
            
        }
        public NetworkRequest(string requestType = "", string data = "")
        {
            RequestType = requestType;
            Data = data;
        }
        public string RequestType { get; set; }
        public string Data { get; set; }
    }
}