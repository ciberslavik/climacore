using System.Collections.Generic;

namespace Clima.Core.IO
{
    public class IOPinCollection
    {
        private Dictionary<string, IAnalogOutput> _analogOutputs;
        private List<IAnalogOutput> _modifiedAnalogOutputs;
        
        private Dictionary<string, IAnalogInput> _analogInputs;
        private Dictionary<string, IDiscreteInput> _discreteInputs;
        private Dictionary<string, IDiscreteOutput> _discreteOutputs;
        private List<IDiscreteOutput> _modifiedDiscreteOutputs;
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
            if (ea.Pin is IDiscreteOutput pin)
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
            if (ea.Pin is IAnalogOutput output)
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
            _analogInputs = new Dictionary<string, IAnalogInput>();
            _analogOutputs = new Dictionary<string, IAnalogOutput>();
            _modifiedAnalogOutputs = new List<IAnalogOutput>();
            _discreteInputs = new Dictionary<string, IDiscreteInput>();
            _discreteOutputs = new Dictionary<string, IDiscreteOutput>();
            _modifiedDiscreteOutputs = new List<IDiscreteOutput>();

            _isAnalogModified = false;
            _isDiscreteModified = false;
        }
        
        public Dictionary<string, IAnalogOutput> AnalogOutputs => _analogOutputs;
        public void AddAnalogOutput(string pinName, IAnalogOutput output)
        {
            output.ValueChanged += OnAnalogOutputChanged;
            _analogOutputs.Add(pinName, output);
        }
        
        public Dictionary<string, IAnalogInput> AnalogInputs => _analogInputs;
        public void AddAnalogInput(string pinName, IAnalogInput input)
        {
            input.ValueChanged += OnAnalogInputChanged;
            _analogInputs.Add(pinName, input);
        }
        
        public Dictionary<string, IDiscreteInput> DiscreteInputs => _discreteInputs;
        public void AddDiscreteInput(string pinName, IDiscreteInput input)
        {
            input.PinStateChanged += OnDiscreteInputChanged;
            _discreteInputs.Add(pinName, input);
        }

        public Dictionary<string, IDiscreteOutput> DiscreteOutputs => _discreteOutputs;

        public bool IsAnalogModified => _isAnalogModified;

        public bool IsDiscreteModified => _isDiscreteModified;

        public List<IAnalogOutput> ModifiedAnalogOutputs => _modifiedAnalogOutputs;

        public List<IDiscreteOutput> ModifiedDiscreteOutputs => _modifiedDiscreteOutputs;

        public void AddDiscreteOutput(string pinName, IDiscreteOutput output)
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