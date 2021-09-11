namespace Clima.Core.Network.Messages
{
    public class RemoveGraphRequest
    {
        public RemoveGraphRequest(string key = "")
        {
            Key = key;
            GraphType = 0;
        }

        public int GraphType { get; set; }
        public string Key { get; set; }
    }
}