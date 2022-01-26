namespace Clima.Core.Controllers.Network.Messages.Light
{
    public class LightStatusResponse
    {
        public string CurrentProfileName { get; set; }
        public string CurrentProfileKey { get; set; }
        public bool IsAuto { get; set; }
        public bool IsOn { get; set; }
    }
}