namespace Clima.Core.Controllers.Network.Messages
{
    public class UpdateServoStateRequest
    {
        public UpdateServoStateRequest()
        {
        }
        public bool IsManual { get; set; }
        public float SetPoint { get; set; }
    }
}