namespace Clima.Core.Network.Messages
{
    public class RemoveGraphRequest
    {
        public RemoveGraphRequest(string graphType = "", string key = "")
        {
            Key = key;
            GraphType = graphType;
        }

        public string GraphType { get; set; }
        public string Key { get; set; }
    }
}