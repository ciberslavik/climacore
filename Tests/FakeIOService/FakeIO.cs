using System;
using System.Collections.Generic;
using Clima.Services.IO;

namespace FakeIOService
{
    public class FakeIO:IIOService
    {
        internal IDictionary<string, DiscreteInput> _discreteInputs;
        internal IDictionary<string, DiscreteOutput> _discreteOutputs;
        internal IDictionary<string, AnalogOutput> _analogOutputs;
        internal IDictionary<string, AnalogInput> _analogInputs;
        private List<FakeControlledRelay> _controlledRelays;
        public FakeIO()
        {
            IsInit = false;
            IsRunning = false;
            _controlledRelays = new List<FakeControlledRelay>();
        }


        public void Init()
        {
            //Create discrete outputs
            _discreteOutputs = new Dictionary<string, DiscreteOutput>();
            for (int i = 0; i < 20; i++)
            {
                var dout =new DiscreteOutput();
                dout.PinName = $"DO:{i}";
                dout.PinStateChanged += DoutOnPinStateChanged;
                _discreteOutputs.Add($"DO:{i}",dout);    
            }
            
            //Create discrete inputs
            _discreteInputs = new Dictionary<string, DiscreteInput>();

            for (int i = 0; i < 16; i++)
            {
                _discreteInputs.Add($"DI:{i}",new DiscreteInput());
            }
            
            _analogOutputs = new Dictionary<string, AnalogOutput>();
            _analogOutputs.Add("AO:0",new AnalogOutput());
            _analogOutputs.Add("AO:1",new AnalogOutput());
            
            _analogInputs = new Dictionary<string, AnalogInput>();

            for (int i = 0; i < 10; i++)
            {
                _analogInputs.Add($"AI:{i}",new AnalogInput());
            }
            //Create FC emulators
            
            
            //Create controlled relay emulators
            for (int i = 0; i < 8; i++)
            {
                _controlledRelays.Add(new FakeControlledRelay(this, i));
            }
            
            IsInit = true;
        }
        
        private void DoutOnPinStateChanged(DiscretePinStateChangedEventArgs args)
        {
            Console.WriteLine($"DO State Changed:{args.Pin.PinName}");
        }

        public void Start()
        {
            IsRunning = true;
        }

        public void Stop()
        {
            IsRunning = false;
        }

        public bool IsInit { get; private set; }
        public bool IsRunning { get; private set; }
        public IOPinCollection Pins { get; }

        public IDictionary<string, DiscreteInput> DiscreteInputs => _discreteInputs;

        public IDictionary<string, DiscreteOutput> DiscreteOutputs => _discreteOutputs;

        public IDictionary<string, AnalogOutput> AnalogOutputs => _analogOutputs;

        public IDictionary<string, AnalogInput> AnalogInputs => _analogInputs;
    }
}