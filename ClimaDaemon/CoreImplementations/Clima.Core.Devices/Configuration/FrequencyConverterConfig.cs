namespace Clima.Core.Devices.Configuration
{
    public class FrequencyConverterConfig
    {
        public FrequencyConverterConfig()
        {
        }
        public ConverterType ConverterType { get; set; }
        public string ConverterName { get; set; }
        
        public string EnablePinName { get; set; }
        public string AlarmPinName { get; set; }
        public string AnalogPinName { get; set; }

        public static FrequencyConverterConfig CreateDefault(string converterName)
        {
            var config = new FrequencyConverterConfig();
            config.ConverterType = ConverterType.Thyristor;
            config.ConverterName = converterName;
            config.EnablePinName = "";
            config.AlarmPinName = "";
            config.AnalogPinName = "AI";
            return config;
        }
    }

    public enum ConverterType
    {
        Frequency,
        Thyristor
    }
}