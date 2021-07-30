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
                din.PinName = $"DO:2:{i}";
                config.DiscreteInputs.Add(din.PinName, din);
            }
            return config;
        }

        public string ConfigurationName => FileName;
        public static string FileName =>"StubIOConfig";
    }
}