using System;
using Clima.Basics.Configuration;
using Clima.Basics.Services;
using Clima.Core.IO;
using Clima.Core.Tests.IOService.Configurations;

namespace Clima.Core.Tests.IOService
{
    public class StubIOService : IIOService
    {
        
        private StubIOServiceConfig _config;

        public StubIOService()
        {
            
        }

        public void Start()
        {
            Console.WriteLine("Stub IO Service Started");
        }

        public void Stop()
        {
            Console.WriteLine("Stub IO Service Stopped");
        }

        public void Init(object config)
        {
            Pins = new IOPinCollection();
            _config = config as StubIOServiceConfig;
            if (_config is not null)
            {
                foreach (var doConfig in _config.DiscreteOutputs.Values)
                {
                    var discrOut = new StubDiscreteOutput();
                    discrOut.PinName = doConfig.PinName;
                    Pins.AddDiscreteOutput(doConfig.PinName, discrOut);
                }

                foreach (var aiConfig in _config.AnalogInputs.Values)
                {
                    var analogIn = new StubAnalogInput();
                    analogIn.PinName = aiConfig.PinName;
                    analogIn.Value = aiConfig.Value;
                    Pins.AnalogInputs.Add(aiConfig.PinName, analogIn);
                }

                Console.WriteLine("Stub IO Service initialised");
            }
        }

        public Type ConfigType => typeof(StubIOServiceConfig);
        public ServiceState ServiceState { get; }

        public bool IsInit { get; }
        public bool IsRunning { get; }
        public IOPinCollection Pins { get; private set; }
    }
}