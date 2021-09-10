using System;
using System.Runtime.CompilerServices;
using Clima.Basics;
using Clima.Basics.Configuration;
using Clima.Core.Devices.Configuration;
using Clima.Core.Exceptions;
using Clima.Core.IO;

namespace Clima.Core.Devices
{
    public class MonitoredRelay : Device, IRelay, IAlarmNotifier
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
        private IDiscreteInput _monitorPin;
        private IDiscreteOutput _enablePin;

        public MonitoredRelay(ITimer timer)
        {
            MonitorPin = null;
            EnablePin = null;
            _monitorTimer = timer;
            _monitorTimer.Interval = 1000;
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

        public MonitoredRelayConfig Configuration { get; set; }

        public IDiscreteInput MonitorPin
        {
            get => _monitorPin;
            set
            {
                _monitorPin = value;
                if (_monitorPin != null)
                    _monitorPin.PinStateChanged += MonitorPinOnPinStateChanged;
            }
        }

        public IDiscreteOutput EnablePin
        {
            get => _enablePin;
            set => _enablePin = value;
        }

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
            _monitorTimer.Interval = Configuration.StateChangeTimeout;
            _monitorTimer.Start();
        }

        public void Off()
        {
            if (Configuration.EnableLevel == ActiveLevel.High)
                EnablePin.SetState(false, true);
            else if (Configuration.EnableLevel == ActiveLevel.Low)
                EnablePin.SetState(true, true);

            _state = RelayState.TryOff;
            if (GetMonitorState(MonitorPin.State))
            {
                _monitorTimer.Interval = Configuration.StateChangeTimeout;
                _monitorTimer.Start();
            }
            else
            {
                _state = RelayState.Off;
            }
        }

        public override void InitDevice(IConfigurationItem deviceConfig)
        {
            if (deviceConfig is MonitoredRelayConfig relayConfig)
            {
                Configuration = relayConfig;

                if (MonitorPin == null || EnablePin == null)
                    throw new DeviceNotConfiguredException("Relay pins not configured");


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
            ClimaContext.Logger.Debug($"monitor from:{ea.PrevState} to {ea.NewState}");
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

        public event AlarmNotifyEventHandler AlarmNotify;
        public string NotifierName => Configuration.RelayName;

        protected virtual void OnAlarmNotify(string message)
        {
            SetEnablePinState(false);
            var ea = new AlarmNotifyEventArgs(message);
            AlarmNotify?.Invoke(this, ea);
        }

        private void SetEnablePinState(bool state)
        {
            if (Configuration.EnableLevel == ActiveLevel.High)
                EnablePin.SetState(state, true);
            else if (Configuration.EnableLevel == ActiveLevel.Low)
                EnablePin.SetState(!state, true);
        }
    }
}