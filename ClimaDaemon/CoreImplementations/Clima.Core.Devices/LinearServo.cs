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

        private double _prevPos;
        public void SetPosition(double position)
        {
            _current = ServoFeedbackPin.Value;
            _target = position;
            _prevPos = _current;
            if (_current > _target)
            {
                ServoClosePin.SetState(true);
            }
            else if (_current < _target)
            {
                ServoOpenPin.SetState(true);
            }
            _pulseTimer = new Timer(TestCAllback,null,10000,-1);
        }

        private void TestCAllback(object? state)
        {
            ServoClosePin.SetState(false);
            ServoOpenPin.SetState(false);
            Logger.Debug($" moving per second:{(_prevPos - _current)/10} per 10 second{_prevPos - _current}");
            
            /*
            [Debug]ServoFeedbackPinOnValueChanged:Servo value:60,6
                [Debug]ServoFeedbackPinOnValueChanged:Servo value:59,7
                [Debug]ServoFeedbackPinOnValueChanged:Servo value:58,9
                [Debug]ServoFeedbackPinOnValueChanged:Servo value:58,1
                [Debug]TestCAllback: moving per second:0,9999999999999993 per 10 second9,999999999999993
                [Debug]ServoFeedbackPinOnValueChanged:Servo value:57,1
                [Debug]ServoFeedbackPinOnValueChanged:Servo value:57
                [Debug]ServoFeedbackPinOnValueChanged:Servo value:57,1
                [Debug]ServoFeedbackPinOnValueChanged:Servo value:57
                [Debug]ServoFeedbackPinOnValueChanged:Servo value:57,1
                */

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
            _current = ea.NewValue;
            /*var minCoarse = _target - Configuration.CoarseAccuracy;
            var maxCoarse = _target + Configuration.CoarseAccuracy;
            if (_current >= minCoarse && _current <= maxCoarse)
            {
                if (_controlState == ControlState.Closing)
                {
                    ServoClosePin.SetState(false);
                    _controlState = ControlState.FineClosing;
                }
                else if (_controlState == ControlState.Opening)
                {
                    ServoOpenPin.SetState(false);
                    _controlState = ControlState.FineOpening;
                }
            }*/
            
            Logger.Debug($"Servo value:{_current}");
        }
        
        internal ISystemLogger Logger { get; set; }

        
        
    }
}