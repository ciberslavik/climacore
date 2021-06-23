namespace Clima.Services.Devices.Configs
{
    public class FCConfig
    {
        public FCConfig()
        {
        }
        public string FCName { get; set; }
        public string EnablePinName { get; set; }
        public string AlarmPinName { get; set; }
        public string AnalogPinName { get; set; }
        public int StartUpTime { get; set; }
    }
}