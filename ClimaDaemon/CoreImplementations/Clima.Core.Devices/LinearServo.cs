using System;
using System.Threading;
using Clima.Basics.Services;
using Clima.Core.Devices.Configuration;
using Clima.Core.IO;

namespace Clima.Core.Devices
{
    public class LinearServo : IServoDrive
    {
        private bool __isMoving;

        private bool _isMoving
        {
            get => __isMoving;
            set
            {
                __isMoving = value;
                Logger.Debug($"IsMoving to:{__isMoving}");
            }
        }

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
            throw new NotImplementedException();
        }

        public void Close()
        {
        }

        public void SetPosition(double target)
        {
            if (__isMoving)
            {
                _isMoving = false;
                ServoClosePin.SetState(false);
                ServoOpenPin.SetState(false);
                if (_fineTimer != null)
                    _fineTimer.Dispose();
            }

            var current = ServoFeedbackPin.Value;
            Logger.Debug($"Set position:{target}");
            //Check need moving
            if (HitFineWindow(current, target))
            {
                Console.WriteLine("Current position in target window");
                return;
            }

            //Evaluate distance to target, and select Fine or Coarse moving
            var coarseDiff = Configuration.CoarseAccuracy * 3;

            var currentDiff = current - target;
            if (currentDiff < 0)
                currentDiff = currentDiff * -1;

            if (currentDiff > coarseDiff)
                MoveCoarse(target);
            else
                MoveFine(target);
        }

        internal void ProcessPosition(double current, double target)
        {
        }

        private void MoveFine(double target)
        {
            //Check moving
            Logger.Debug("Move Fine");
            if (_isMoving)
                return;
            var current = ServoFeedbackPin.Value;
            //Check move needed
            if (HitFineWindow(current, target))
                return;
            //Evaluate moving direction
            if (current > target)
                _isFineClosing = true;
            else if (current < target) _isFineClosing = false;

            _isFine = true;
            _isMoving = true;
            _targetPos = target;
            _fineTimer = new Timer(FinePauseTimeout, null, Configuration.FinePauseTime, -1);
        }

        internal void MoveCoarse(double target)
        {
            Logger.Debug("Move Coarse");
            if (_isMoving)
                return;
            var current = ServoFeedbackPin.Value;
            _targetPos = target;
            //Evaluate moving direction
            if (current > target)
                //Moving close
                ServoClosePin.SetState(true);
            else if (current < target)
                //Moving open
                ServoOpenPin.SetState(true);
            _isFine = false;
            _isMoving = true;
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
            Logger.Info($"Servo value:{current}");

            if (_isMoving)
            {
                if (_isFine)
                {
                    if (HitFineWindow(current, _targetPos))
                    {
                        ServoClosePin.SetState(false);
                        ServoOpenPin.SetState(false);
                        _fineTimer?.Dispose();
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
            else
            {
                if (!HitFineWindow(current, _targetPos)) MoveFine(_targetPos);
            }
        }

        private void FinePauseTimeout(object o)
        {
            Logger.Debug("Pause timeout");
            if (_isMoving && _isFine)
            {
                var current = ServoFeedbackPin.Value;
                var pulseTime = Configuration.FinePulseTime;

                Logger.Debug($"Pulse time:{pulseTime}");

                if (current > _targetPos)
                    _isFineClosing = true;
                else
                    _isFineClosing = false;


                if (_isFineClosing)
                    ServoClosePin.SetState(true);
                else
                    ServoOpenPin.SetState(true);

                _fineTimer.Dispose();
                _fineTimer = new Timer(FinePulseTimeout, null, pulseTime, -1);
            }
        }

        private void FinePulseTimeout(object o)
        {
            Logger.Debug("Pulse timeout");
            var current = ServoFeedbackPin.Value;

            if (_isMoving && _isFine)
            {
                _fineTimer.Dispose();
                ServoClosePin.SetState(false);
                ServoOpenPin.SetState(false);

                if (HitFineWindow(current, _targetPos))
                {
                    _isFine = false;
                    _isMoving = false;
                }
                else
                {
                    if (current > _targetPos)
                        _isFineClosing = true;
                    else
                        _isFineClosing = false;

                    _fineTimer = new Timer(FinePauseTimeout, null, Configuration.FinePauseTime, -1);
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