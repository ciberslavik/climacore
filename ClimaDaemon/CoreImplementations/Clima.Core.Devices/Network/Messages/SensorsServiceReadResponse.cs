namespace Clima.Core.Devices.Network.Messages
{
    public class SensorsServiceReadResponse
    {
        public string FrontTemperature { get; set; }
        public string RearTemperature { get; set; }
        public string OutdoorTemperature { get; set; }
        public string Humidity { get; set; }
        public string Presure { get; set; }
    }
}