using System.Collections.Generic;

namespace Clima.Services.IO
{
    public class IOPinCollection
    {
        private Dictionary<string, AnalogOutput> _analogOutputs;
        private Dictionary<string, AnalogInput> _analogInputs;
        private Dictionary<string, DiscreteInput> _discreteInputs;
        private Dictionary<string, DiscreteOutput> _discreteOutputs;
        
        public IOPinCollection()
        {
            _analogInputs = new Dictionary<string, AnalogInput>();
            _analogOutputs = new Dictionary<string, AnalogOutput>();
            _discreteInputs = new Dictionary<string, DiscreteInput>();
            _discreteOutputs = new Dictionary<string, DiscreteOutput>();
            
        }


        public Dictionary<string, AnalogOutput> AnalogOutputs => _analogOutputs;

        public void AddAnalogOutput(string pinName, AnalogOutput output)
        {
            _analogOutputs.Add(pinName, output);
            output.ValueChanged+= OnOutputValueChanged;
        }
        private void OnOutputValueChanged(AnalogPinValueChangedEventArgs ea)
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<string, AnalogInput> AnalogInputs => _analogInputs;

        public void AddAnalogInput(string pinName, AnalogInput input)
        {
            _analogInputs.Add(pinName, input);
        }

        public Dictionary<string, DiscreteInput> DiscreteInputs => _discreteInputs;

        public void AddDiscreteInput(string pinName, DiscreteInput input)
        {
            _discreteInputs.Add(pinName, input);
        }

        public Dictionary<string, DiscreteOutput> DiscreteOutputs => _discreteOutputs;

        public void AddDiscreteOutput(string pinName, DiscreteOutput output)
        {
            output.PinStateChanged += OnOutputStateChanged;
            _discreteOutputs.Add(pinName, output);
        }

        private void OnOutputStateChanged(DiscretePinStateChangedEventArgs ea)
        {
            throw new System.NotImplementedException();
        }
    }
}