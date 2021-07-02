using System;
using System.Timers;
using Clima.DataModel.Configurations.IOSystem;
using Clima.Services.Alarm;
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
                EnablePin.SetState(true, true);
                _startTimer.Interval = StartUpTime;
                _startTimer.Start();
            }
        }

        public void StopFC()
        {
            if (IsRunning)
            {
                AnalogPin.SetValue(0);
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
                    AnalogPin.SetValue(value);
                }
                else
                {
                    _value = value;
                }
            }
        }
        internal IDiscreteOutput EnablePin { get; set; }
        internal IDiscreteInput AlarmPin { get; set; }
        internal IAnalogOutput AnalogPin { get; set; }
        public override void InitDevice(DeviceConfigBase deviceConfig)
        {
            
        }

        private void OnAlarmStateChanged(DiscretePinStateChangedEventArgs args)
        {
            if (IsRunning)
            {
                AlarmNotify?.Invoke(new AlarmNotifyEventArgs($"Ошибка частотного преобразователя"));
                State = FCStateEnum.Alarm;
            }
        }

        private void StartTimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            if (State == FCStateEnum.Starting)
            {
                if (!AlarmPin.State)
                {
                    AnalogPin.SetValue(_value);
                    IsRunning = true;
                    State = FCStateEnum.Running;
                }
            }
            else if (State == FCStateEnum.Stopping)
            {
                EnablePin.SetState(false,true);
                State = FCStateEnum.Stopped;
                IsRunning = false;
            }
        }
        
    }
}