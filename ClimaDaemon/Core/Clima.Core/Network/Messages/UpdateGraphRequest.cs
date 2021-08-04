namespace Clima.Core.Network.Messages
{
    public class UpdateGraphRequest
    {
        public UpdateGraphRequest()
        {
        }
        public string GraphType { get; set; }
        public string GraphKey { get; set; }
        public string Data { get; set; }
    }
}