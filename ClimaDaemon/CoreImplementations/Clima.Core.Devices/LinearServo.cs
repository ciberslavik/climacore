using System;
using System.Threading;
using Clima.Basics.Services;
using Clima.Core.Devices.Configuration;
using Clima.Core.IO;

namespace Clima.Core.Devices
{
    public class LinearServo:IServoDrive
    {
        
        private bool _isMoving;
        private bool _isFine;
        private bool _isFineClosing;
        private double _targetPos;
        //private double _current;
        
        private Timer _fineTimer;
        private IAnalogInput _servoFeedbackPin;
        
        
        public LinearServo()
        {
            
        }
        

        public void Open()
        {
            throw new System.NotImplementedException();
        }

        public void Close()
        {
            
        }

        private double _prevPos;
        public void SetPosition(double target)
        {
            var current = ServoFeedbackPin.Value;
            
            //Check need moving
            if (HitFineWindow(current, target)) 
            {
                Console.WriteLine("Current position in target window");
                return;
            }

            //Evaluate distance to target, and select Fine or Coarse moving
            var coarseDiff = Configuration.CoarseAccuracy * 3;
            var currentDiff = (current - target) * -1;
            
            if (currentDiff > coarseDiff)
                MoveCoarse(target);
            else
                MoveFine(target);
            
        }

        internal void ProcessPosition(double current, double target)
        {
            
        }

        private void MoveFine(double target)
        {   //Check moving
            if(_isMoving)
                return;
            var current = ServoFeedbackPin.Value;
            //Check move needed
            if (HitFineWindow(current, target))
                return;
            //Evaluate moving direction
            if (current > target)   
            {
                _isFineClosing = true;
            }
            else if (current < target)
            {
                _isFineClosing = false;
            }

            _isFine = true;
            _isMoving = true;
            _targetPos = target;
            _fineTimer = new Timer(FinePauseTimeout, null, Configuration.FinePulseTime, -1);
        }

        internal void MoveCoarse(double target)
        {
            if(_isMoving)
                return;
            var current = ServoFeedbackPin.Value;
            _targetPos = target;
            //Evaluate moving direction
            if (current > target)
            {
                //Moving close
                ServoClosePin.SetState(true);
            }
            else if(current < target)
            {
                //Moving open
                ServoOpenPin.SetState(true);
            }
            _isFine = false;
            _isMoving = true;
            
        }

        private void TestCAllback(object? state)
        {
            /*ServoClosePin.SetState(false);
            ServoOpenPin.SetState(false);
            Logger.Debug($" moving per second:{(_prevPos - _current)/10} per 10 second{_prevPos - _current}");
            */
            
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
        
        public double CurrentPosition => ServoFeedbackPin.Value;
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
            var current = ea.NewValue;
            if (_isMoving)
            {
                if (_isFine)
                {
                    if (HitFineWindow(current, _targetPos))
                    {
                        ServoClosePin.SetState(false);
                        ServoOpenPin.SetState(false);
                        _isMoving = false;
                    }
                }
                else
                {
                    //If coarse window reached, stop moving and start fine moving
                    if (HitCoarseWindow(current, _targetPos))
                    {
                        ServoClosePin.SetState(false);
                        ServoOpenPin.SetState(false);
                        _isMoving = false;
                        MoveFine(_targetPos);
                    }
                }
            }
            Logger.Debug($"Servo value:{current}");
        }

        private void FinePauseTimeout(object o)
        {
            if (_isMoving && _isFine)
            {
                if (_isFineClosing)
                {
                    ServoClosePin.SetState(true);
                }
                else
                {
                    ServoOpenPin.SetState(true);
                }

                _fineTimer = new Timer(FinePulseTimeout, null, Configuration.FinePulseTime, -1);
            }
        }

        private void FinePulseTimeout(object o)
        {
            var current = ServoFeedbackPin.Value;
            
            if (_isMoving && _isFine)
            {
                ServoClosePin.SetState(false);
                ServoOpenPin.SetState(false);
                
                if (HitFineWindow(current, _targetPos))
                {
                    _isFine = false;
                    _isMoving = false;
                }
                else
                {
                    _fineTimer = new Timer(FinePauseTimeout, null, Configuration.FinePulseTime, -1);
                }
            }
        }
        internal ISystemLogger Logger { get; set; }

        private bool HitFineWindow(double current, double target)
        {
            var minTarget = target - Configuration.FineAccuracy;
            var maxTarget = target + Configuration.FineAccuracy;
            if (current >= minTarget && current <= maxTarget)
                return true;
            
            return false;
        }
        private bool HitCoarseWindow(double current, double target)
        {
            var minTarget = target - Configuration.CoarseAccuracy;
            var maxTarget = target + Configuration.CoarseAccuracy;
            if (current >= minTarget && current <= maxTarget)
                return true;
            
            return false;
        }
    }
}