using System;
using System.Runtime.CompilerServices;
using Clima.DataModel.Configurations.IOSystem;
using Clima.Services.Alarm;
using Clima.Services.Devices.Configs;
using Clima.Services.Devices.Exceptions;
using Clima.Services.IO;

namespace Clima.Services.Devices
{
    public class Relay : Device, IAlarmNotifier
    {
        private enum RelayState
        {
            None,
            Init,
            TryOn,
            On,
            TryOff,
            Off,
            Alarm
        }

        private readonly ITimer _monitorTimer;
        private RelayState _state;

        public Relay(ITimer timer)
        {
            MonitorPin = null;
            EnablePin = null;
            _monitorTimer = timer;
            _monitorTimer.Elapsed += MonitorTimerOnElapsed;

            //_monitorTimer.
            _state = RelayState.None;
        }

        private void MonitorTimerOnElapsed(object sender)
        {
            if (_state == RelayState.TryOff)
            {
                if (GetMonitorState(MonitorPin.State))
                {
                    _state = RelayState.Alarm;
                    OnAlarmNotify($"Рэле {Configuration.RelayName} не отключилось в течении времени ожидания");
                }

                _state = RelayState.Off;
            }
            else if (_state == RelayState.TryOn)
            {
                if (!GetMonitorState(MonitorPin.State))
                {
                    _state = RelayState.Alarm;
                    OnAlarmNotify(
                        $"Рэле {Configuration.RelayName} не включилось после подачи команды в течении времени ожидания {Configuration.MonitorTimeout}");
                }

                _state = RelayState.Off;
            }
        }

        public RelayConfig Configuration { get; set; }

        public IDiscreteInput MonitorPin { get; set; }

        public IDiscreteOutput EnablePin { get; set; }

        public bool RelayIsOn
        {
            get
            {
                if (_state == RelayState.On)
                    return true;
                return false;
            }
        }

        public void On()
        {
            if (Configuration.EnableLevel == ActiveLevel.High)
            {
                if (!EnablePin.State) EnablePin.SetState(true, true);
            }
            else if (Configuration.EnableLevel == ActiveLevel.Low)
            {
                if (EnablePin.State) EnablePin.SetState(false, true);
            }

            _state = RelayState.TryOn;
            _monitorTimer.Start();
        }

        public void Off()
        {
            if (Configuration.EnableLevel == ActiveLevel.High)
                EnablePin.SetState(false, true);
            else if (Configuration.EnableLevel == ActiveLevel.Low) 
                EnablePin.SetState(true, true);

            _state = RelayState.TryOff;

            _monitorTimer.Start();
        }

        public override void InitDevice(DeviceConfigBase deviceConfig)
        {
            if (deviceConfig is RelayConfig relayConfig)
            {
                Configuration = relayConfig;

                if (MonitorPin == null || EnablePin == null)
                    throw new DevicePinsNotConfiguredException("Relay pins not configured");


                _state = RelayState.Init;
                MonitorPin.PinStateChanged += MonitorPinOnPinStateChanged;
                _monitorTimer.Interval = Configuration.MonitorTimeout;
                Off();
            }
            else
            {
                throw new ArgumentException("Configuration is not a RelayConfig", nameof(deviceConfig));
            }
        }

        private void MonitorPinOnPinStateChanged(DiscretePinStateChangedEventArgs ea)
        {
            if (_state == RelayState.TryOn)
            {
                if (GetMonitorState(ea.NewState))
                {
                    _monitorTimer.Stop();
                    _state = RelayState.On;
                }
            }
            else if (_state == RelayState.On)
            {
                if (!GetMonitorState(ea.NewState))
                {
                    _state = RelayState.Alarm;
                    OnAlarmNotify($"Рэле {Configuration.RelayName} отключилось во время работы");
                }
            }
            else if (_state == RelayState.TryOff)
            {
                if (!GetMonitorState(ea.NewState))
                {
                    _state = RelayState.Off;
                    _monitorTimer.Stop();
                }
            }
            else if (_state == RelayState.Off)
            {
                if (GetMonitorState(ea.NewState))
                {
                    _state = RelayState.Alarm;
                    OnAlarmNotify($"Рэле {Configuration.RelayName} включилось в ручном режиме");
                }
            }
        }

        private bool GetMonitorState(bool pinState)
        {
            var monitorState = false;
            if (Configuration.MonitorLevel == ActiveLevel.High)
            {
                if (pinState)
                    monitorState = true;
            }
            else if (Configuration.MonitorLevel == ActiveLevel.Low)
            {
                if (!pinState)
                    monitorState = true;
            }

            return monitorState;
        }

        public event AlarmNotifyHandler AlarmNotify;

        protected virtual void OnAlarmNotify(string message, [CallerMemberName] string callerName = "")
        {
            AlarmNotify?.Invoke(new AlarmNotifyEventArgs(message, callerName));
        }
    }
}