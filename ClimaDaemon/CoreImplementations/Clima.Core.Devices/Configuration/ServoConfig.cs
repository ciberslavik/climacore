namespace Clima.Core.Devices.Configuration
{
    public class ServoConfig
    {
        public ServoConfig()
        {
        }

        public string ServoName { get; set; }
        public string OpenPinName { get; set; }
        public string ClosePinName { get; set; }
        public string FeedbackPinName { get; set; }
        public double FineAccuracy { get; set; }
        public double CoarseAccuracy { get; set; }
        public static ServoConfig CreateDefault(int servoNumber)
        {
            var config = new ServoConfig();
            config.ServoName = $"SERVO:{servoNumber}";
            config.OpenPinName = $"DO:3:3";
            config.ClosePinName = $"DO:3:4";
            config.FeedbackPinName = $"AI:1:4";
            config.CoarseAccuracy = 1.5;
            config.FineAccuracy = 0.1;
            
            return config;
        }
    }
}