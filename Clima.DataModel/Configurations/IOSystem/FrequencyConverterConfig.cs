namespace Clima.DataModel.Configurations.IOSystem
{
    public class FrequencyConverterConfig:DeviceConfigBase
    {
        public FrequencyConverterConfig()
        {
        }

        public static FrequencyConverterConfig CreateDefault()
        {
            var config = new FrequencyConverterConfig();
            config.ConverterName = "FC:1";
            config.EnablePinName = "DO:1:1";
            config.AlarmPinName = "DI:1:1";
            config.AnalogPinName = "AO:1:1";
            config.StartUpTime = 1000;
            return config;
        }
        public string ConverterName { get; set; }
        public string EnablePinName { get; set; }
        public string AlarmPinName { get; set; }
        public string AnalogPinName { get; set; }
        public int StartUpTime { get; set; }
    }
}