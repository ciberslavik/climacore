using System.Collections.Generic;

namespace Clima.Services.IO
{
    public class IOPinCollection
    {
        private Dictionary<string, AnalogOutput> _analogOutputs;
        private Dictionary<string, AnalogInput> _analogInputs;
        private Dictionary<string, DiscreteInput> _discreteInputs;
        private Dictionary<string, DiscreteOutput> _discreteOutputs;
        #region Events
        public event DiscretePinStateChangedEventHandler DiscreteInputChanged;
        protected virtual void OnDiscreteInputChanged(DiscretePinStateChangedEventArgs ea)
        {
            DiscreteInputChanged?.Invoke(ea);
        }
        public event DiscretePinStateChangedEventHandler DiscreteOutputChanged;
        protected virtual void OnDiscreteOutputChanged(DiscretePinStateChangedEventArgs ea)
        {
            DiscreteOutputChanged?.Invoke(ea);
        }
        public event AnalogPinValueChangedEventHandler AnalogOutputChanged;
        protected virtual void OnAnalogOutputChanged(AnalogPinValueChangedEventArgs ea)
        {
            AnalogOutputChanged?.Invoke(ea);
        }
        public event AnalogPinValueChangedEventHandler AnalogInputChanged;
        protected virtual void OnAnalogInputChanged(AnalogPinValueChangedEventArgs ea)
        {
            AnalogInputChanged?.Invoke(ea);
        }
        
        #endregion Events
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
            output.ValueChanged += OnAnalogOutputChanged;
            _analogOutputs.Add(pinName, output);
        }
        
        public Dictionary<string, AnalogInput> AnalogInputs => _analogInputs;
        public void AddAnalogInput(string pinName, AnalogInput input)
        {
            input.ValueChanged += OnAnalogInputChanged;
            _analogInputs.Add(pinName, input);
        }
        
        public Dictionary<string, DiscreteInput> DiscreteInputs => _discreteInputs;
        public void AddDiscreteInput(string pinName, DiscreteInput input)
        {
            input.PinStateChanged += OnDiscreteInputChanged;
            _discreteInputs.Add(pinName, input);
        }

        public Dictionary<string, DiscreteOutput> DiscreteOutputs => _discreteOutputs;

        public void AddDiscreteOutput(string pinName, DiscreteOutput output)
        {
            output.PinStateChanged += OnDiscreteOutputChanged;
            _discreteOutputs.Add(pinName, output);
        }
    }
}