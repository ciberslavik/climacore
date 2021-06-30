using System.Collections.Generic;

namespace Clima.Services.IO
{
    public class IOPinCollection
    {
        private Dictionary<string, AnalogOutput> _analogOutputs;
        private List<AnalogOutput> _modifiedAnalogOutputs;
        
        private Dictionary<string, AnalogInput> _analogInputs;
        private Dictionary<string, DiscreteInput> _discreteInputs;
        private Dictionary<string, DiscreteOutput> _discreteOutputs;
        private List<DiscreteOutput> _modifiedDiscreteOutputs;
        private bool _isAnalogModified;
        private bool _isDiscreteModified;
        #region Events
        public event DiscretePinStateChangedEventHandler DiscreteInputChanged;
        protected virtual void OnDiscreteInputChanged(DiscretePinStateChangedEventArgs ea)
        {
            DiscreteInputChanged?.Invoke(ea);
        }
        public event DiscretePinStateChangedEventHandler DiscreteOutputChanged;
        protected virtual void OnDiscreteOutputChanged(DiscretePinStateChangedEventArgs ea)
        {
            if (ea.Pin is DiscreteOutput pin)
            {
                if (ModifiedDiscreteOutputs.Contains(pin))
                {
                    ModifiedDiscreteOutputs[ModifiedDiscreteOutputs.IndexOf(pin)] = pin;
                }
                else
                {
                    ModifiedDiscreteOutputs.Add(pin);
                }
            }

            _isDiscreteModified = true;
            DiscreteOutputChanged?.Invoke(ea);
        }
        public event AnalogPinValueChangedEventHandler AnalogOutputChanged;
        protected virtual void OnAnalogOutputChanged(AnalogPinValueChangedEventArgs ea)
        {
            if (ea.Pin is AnalogOutput output)
            {
                if (ModifiedAnalogOutputs.Contains(output))
                {
                    ModifiedAnalogOutputs[ModifiedAnalogOutputs.IndexOf(output)] = output;
                }
                else
                {
                    ModifiedAnalogOutputs.Add(output);
                }
            }
            _isAnalogModified = true;
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
            _modifiedAnalogOutputs = new List<AnalogOutput>();
            _discreteInputs = new Dictionary<string, DiscreteInput>();
            _discreteOutputs = new Dictionary<string, DiscreteOutput>();
            _modifiedDiscreteOutputs = new List<DiscreteOutput>();

            _isAnalogModified = false;
            _isDiscreteModified = false;
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

        public bool IsAnalogModified => _isAnalogModified;

        public bool IsDiscreteModified => _isDiscreteModified;

        public List<AnalogOutput> ModifiedAnalogOutputs => _modifiedAnalogOutputs;

        public List<DiscreteOutput> ModifiedDiscreteOutputs => _modifiedDiscreteOutputs;

        public void AddDiscreteOutput(string pinName, DiscreteOutput output)
        {
            output.PinStateChanged += OnDiscreteOutputChanged;
            _discreteOutputs.Add(pinName, output);
        }

        public void AcceptDiscrete()
        {
            ModifiedDiscreteOutputs.Clear();
            _isDiscreteModified = false;
        }

        public void AcceptAnalog()
        {
            ModifiedAnalogOutputs.Clear();
            _isAnalogModified = false;
        }
    }
}