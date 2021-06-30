using System;
using System.Timers;
using Clima.Core.Alarm;
using Clima.Services.Devices.Configs;
using Clima.Services.IO;

namespace Clima.Services.Devices
{
    public class FrequencyConverter:Device,IAlarmNotifier
    {
        private Timer _startTimer;
        private double _value;
        private bool _stopping;
        public event AlarmNotifyHandler AlarmNotify;
        public FrequencyConverter()
        {
            _startTimer = new Timer();
            _startTimer.Elapsed+= StartTimerOnElapsed;
            _value = 0;
            State = FCStateEnum.Stopped;
        }
        public FCStateEnum State { get; private set; }
        public int StartUpTime { get; private set; }
        public bool IsRunning { get; private set; }

        public void StartFC()
        {
            if(IsRunning)
                return;
            
            if (AlarmPin.State == false)
            {
                EnablePin.State = true;
                _startTimer.Interval = StartUpTime;
                _startTimer.Start();
            }
        }

        public void StopFC()
        {
            if (IsRunning)
            {
                AnalogPin.Value = 0;
                State = FCStateEnum.Stopping;
                _startTimer.Start();
            }
        }

        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (IsRunning)
                {
                    AnalogPin.Value = value;
                }
                else
                {
                    _value = value;
                }
            }
        }
        internal DiscreteOutput EnablePin { get; set; }
        internal DiscreteInput AlarmPin { get; set; }
        internal AnalogOutput AnalogPin { get; set; }
        public override void InitDevice()
        {
            
        }

        private void OnAlarmStateChanged(DiscretePinStateChangedEventArgs args)
        {
            if (IsRunning)
            {
                AlarmNotify?.Invoke(new AlarmNotifyEventArgs());
                State = FCStateEnum.Alarm;
            }
        }

        private void StartTimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            if (State == FCStateEnum.Starting)
            {
                if (!AlarmPin.State)
                {
                    AnalogPin.Value = _value;
                    IsRunning = true;
                    State = FCStateEnum.Running;
                }
            }
            else if (State == FCStateEnum.Stopping)
            {
                EnablePin.State = false;
                State = FCStateEnum.Stopped;
                IsRunning = false;
            }
        }
        
    }
}