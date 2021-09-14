namespace Clima.Core.Controllers.Network.Messages
{
    public class ServoStateResponse
    {
        public ServoStateResponse()
        {
        }
        public bool IsManual { get; set; }
        public float CurrentPosition { get; set; }
        public float SetPoint { get; set; }
    }
}