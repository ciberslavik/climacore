using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Clima.Core.IO
{
    public sealed class IOPinCollection
    {
        private Dictionary<string, IAnalogOutput> _analogOutputs;

        private Dictionary<string, IAnalogInput> _analogInputs;
        private Dictionary<string, IDiscreteInput> _discreteInputs;
        private Dictionary<string, IDiscreteOutput> _discreteOutputs;
        private bool _isAnalogModified;
        private bool _isDiscreteModified;

        #region Events

        public event DiscretePinStateChangedEventHandler DiscreteInputChanged;

        private void OnDiscreteInputChanged(DiscretePinStateChangedEventArgs ea)
        {
            DiscreteInputChanged?.Invoke(ea);
        }

        public event DiscretePinStateChangedEventHandler DiscreteOutputChanged;

        private void OnDiscreteOutputChanged(DiscretePinStateChangedEventArgs ea)
        {
            _isDiscreteModified = true;
            DiscreteOutputChanged?.Invoke(ea);
        }

        public event AnalogPinValueChangedEventHandler AnalogOutputChanged;

        private void OnAnalogOutputChanged(AnalogPinValueChangedEventArgs ea)
        {
            if (ea.Pin is IAnalogOutput output)
            {
            }

            _isAnalogModified = true;
            AnalogOutputChanged?.Invoke(ea);
        }

        public event AnalogPinValueChangedEventHandler AnalogInputChanged;

        private void OnAnalogInputChanged(AnalogPinValueChangedEventArgs ea)
        {
            AnalogInputChanged?.Invoke(ea);
        }

        #endregion Events

        public IOPinCollection()
        {
            _analogInputs = new Dictionary<string, IAnalogInput>();
            _analogOutputs = new Dictionary<string, IAnalogOutput>();

            _discreteInputs = new Dictionary<string, IDiscreteInput>();
            _discreteOutputs = new Dictionary<string, IDiscreteOutput>();

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

        public void AddDiscreteOutput(string pinName, IDiscreteOutput output)
        {
            output.PinStateChanged += OnDiscreteOutputChanged;
            _discreteOutputs.Add(pinName, output);
        }

        public void AcceptDiscrete()
        {
            _isDiscreteModified = false;
        }

        public void AcceptAnalog()
        {
            _isAnalogModified = false;
        }
    }
}