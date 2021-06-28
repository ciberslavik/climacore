namespace Clima.CommandProcessor.Requests
{
    public class NetworkRequest
    {
        public NetworkRequest()
        {
            
        }
        public NetworkRequest(string requestName = "", string data = "")
        {
            RequestName = requestName;
            Data = data;
        }
        public string RequestName { get; set; }
        public string Data { get; set; }
    }
}