using System.Threading;
using Clima.Basics.Services;
using Clima.Core.Devices.Configuration;
using Clima.Core.IO;

namespace Clima.Core.Devices
{
    public class LinearServo:IServoDrive
    {
        private enum ControlState
        {
            Opening,
            Closing,
            FineOpening,
            FineClosing,
            Standby
        }

        private ControlState _controlState;
        private double _target;
        private double _current;
        private Timer _pulseTimer;
        private IAnalogInput _servoFeedbackPin;
        
        
        public LinearServo()
        {
            _controlState = ControlState.Standby;
        }
        

        public void Open()
        {
            throw new System.NotImplementedException();
        }

        public void Close()
        {
            
        }

        public void SetPosition(double position)
        {
            _current = ServoFeedbackPin.Value;
            _target = position;
            
            if (_current > _target)
            {
                _controlState = ControlState.Closing;
            }
            else if (_current < _target)
            {
                _controlState = ControlState.Opening;
            }
        }

        public double CurrentPosition => _current;
        internal ServoConfig Configuration { get; set; }
        internal IDiscreteOutput ServoOpenPin { get; set; }
        internal IDiscreteOutput ServoClosePin { get; set; }
        internal IAnalogInput ServoFeedbackPin
        {
            get => _servoFeedbackPin;
            set
            {
                _servoFeedbackPin = value;
                _servoFeedbackPin.ValueChanged += ServoFeedbackPinOnValueChanged;  
            }
        }

        private void ServoFeedbackPinOnValueChanged(AnalogPinValueChangedEventArgs ea)
        {
            var minCoarseTarget = _target - Configuration.CoarseAccuracy;
            var maxCoarseTarget = _target + Configuration.CoarseAccuracy;

        }

        private void OnAnalogReady(IAnalogInput input)
        {
            
        }
        private void OpenPulse(int pulseTime)
        {
            _state.IsOpening = true;
            _state.ProcessPulse = true;
            _pulseTimer = new Timer(PrePulseTimeout, null, pulseTime, -1);
        }

        private void ClosePulse(int pulseTime)
        {
            _state.IsOpening = false;
            _state.ProcessPulse = true;
            _pulseTimer = new Timer(PrePulseTimeout, null, pulseTime, -1);
        }

        private void PrePulseTimeout(object o)
        {
            if(_state.IsOpening)
                ServoOpenPin.SetState(true);
            else
                ServoClosePin.SetState(true);

            _pulseTimer = new Timer(PulseTimeout, null, 500, -1);
        }
        private void PulseTimeout(object o)
        {
            _state.ProcessPulse = false;
            ServoClosePin.SetState(false);
            ServoOpenPin.SetState(false);
        }
        internal ISystemLogger Logger { get; set; }

        
        
    }
}