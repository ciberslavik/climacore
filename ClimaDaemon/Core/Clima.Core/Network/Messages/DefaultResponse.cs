namespace Clima.Core.Network.Messages
{
    public class DefaultResponse
    {
        public DefaultResponse(string status = "OK")
        {
            Status = status;
            RequestName = "";
        }

        public string RequestName { get; set; }
        public string Status { get; set; }
    }
}