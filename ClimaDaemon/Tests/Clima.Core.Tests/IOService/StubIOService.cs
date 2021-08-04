using System;
using Clima.Basics.Configuration;
using Clima.Core.IO;
using Clima.Core.Tests.IOService.Configurations;

namespace Clima.Core.Tests.IOService
{
    public class StubIOService:IIOService
    {
        private readonly IConfigurationStorage _configStore;
        private StubIOServiceConfig _config;
        
        public StubIOService(IConfigurationStorage configStore)
        {
            _configStore = configStore;
            if (_configStore.Exist("StubIOConfig"))
            {
                _config = _configStore.GetConfig<StubIOServiceConfig>("StubIOConfig");
            }
            else
            {
                _config = StubIOServiceConfig.CreateDefault();
                _configStore.RegisterConfig("StubIOConfig", _config);
            }
            
        }
        public void Init()
        {
            Pins = new IOPinCollection();

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

        public void Start()
        {
            Console.WriteLine("Stub IO Service Started");
        }

        public void Stop()
        {
            Console.WriteLine("Stub IO Service Stopped");
        }

        public bool IsInit { get; }
        public bool IsRunning { get; }
        public IOPinCollection Pins { get; private set; }
    }
}