using System.Collections.Generic;
using Clima.Basics.Configuration;

namespace Clima.Core.Tests.IOService.Configurations
{
    public class StubIOServiceConfig:IConfigurationItem
    {
        public Dictionary<string, StubAINConfig> AnalogInputs { get; set; } = new Dictionary<string, StubAINConfig>();

        public Dictionary<string, StubAOUTConfig> AnalogOutputs { get; set; } =
            new Dictionary<string, StubAOUTConfig>();

        public Dictionary<string, StubDINConfig> DiscreteInputs { get; set; } = new Dictionary<string, StubDINConfig>();

        public Dictionary<string, StubDOUTConfig> DiscreteOutputs { get; set; } =
            new Dictionary<string, StubDOUTConfig>();

        public static StubIOServiceConfig CreateDefault()
        {
            var config = new StubIOServiceConfig();
            for (int i = 0; i < 12; i++)
            {
                var dout = new StubDOUTConfig();
                dout.PinName = $"DO:2:{i}";
                config.DiscreteOutputs.Add(dout.PinName, dout);
            }
            
            for (int i = 0; i < 12; i++)
            {
                var din = new StubDINConfig();
                din.PinName = $"DI:2:{i}";
                config.DiscreteInputs.Add(din.PinName, din);
            }

            for (int i = 0; i < 8; i++)
            {
                var ain = new StubAINConfig();
                ain.PinName = $"AI:1:{i}";
                ain.Value = 36.6;
                config.AnalogInputs.Add(ain.PinName, ain);
            }
            return config;
        }

        public string ConfigurationName => FileName;
        public static string FileName =>"StubIOConfig";
    }
}