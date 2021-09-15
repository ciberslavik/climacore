using System;
using Castle.Core.Internal;
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
                foreach (var diConfig in _config.DiscreteInputs.Values)
                {
                    var di = new StubDiscreteInput();
                    di.PinName = diConfig.PinName;
                    Pins.AddDiscreteInput(diConfig.PinName, di);
                }
                foreach (var doConfig in _config.DiscreteOutputs.Values)
                {
                    var discrOut = new StubDiscreteOutput();
                    discrOut.PinName = doConfig.PinName;
                    
                    if(!doConfig.MonitorPinName.IsNullOrEmpty())
                        discrOut.MonitorPin = (StubDiscreteInput)Pins.DiscreteInputs[doConfig.MonitorPinName];
                    
                    Pins.AddDiscreteOutput(doConfig.PinName, discrOut);
                }

                foreach (var aiConfig in _config.AnalogInputs.Values)
                {
                    var analogIn = new StubAnalogInput();
                    analogIn.PinName = aiConfig.PinName;
                    analogIn.Value = aiConfig.Value;
                    Pins.AnalogInputs.Add(aiConfig.PinName, analogIn);
                }

                foreach (var aoConfig in _config.AnalogOutputs.Values)
                {
                    var analogOut = new StubAnalogOutput();
                    analogOut.PinName = aoConfig.PinName;
                    Pins.AnalogOutputs.Add(aoConfig.PinName, analogOut);
                }
                Console.WriteLine("Stub IO Service initialised");
                IsInit = true;
            }
        }

        public Type ConfigType => typeof(StubIOServiceConfig);
        public ServiceState ServiceState { get; }

        public bool IsInit { get; private set; }
        public bool IsRunning { get; }
        public IOPinCollection Pins { get; private set; }
    }
}