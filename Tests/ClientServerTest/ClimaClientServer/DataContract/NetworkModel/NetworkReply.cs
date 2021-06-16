namespace DataContract.NetworkModel
{
    public class NetworkReply
    {
        public NetworkReply()
        {
            ReplyType = "GetInfo";
            Data = "";
        }
        public string ReplyType { get; set; }
        public string Data { get; set; }
    }
}