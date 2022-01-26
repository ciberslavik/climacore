using System;
using System.Runtime.CompilerServices;
using Clima.Basics;
using Clima.Basics.Configuration;
using Clima.Core.Alarm;
using Clima.Core.Devices.Configuration;
using Clima.Core.Exceptions;
using Clima.Core.IO;

namespace Clima.Core.Devices
{
    public class MonitoredRelay : IRelay,IAlarmNotifier
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
        private readonly MonitoredRelayConfig _config;
        private RelayState _state;
        private IDiscreteInput _monitorPin;
        private IDiscreteOutput _enablePin;
        public MonitoredRelay(ITimer timer, MonitoredRelayConfig config)
        {
            _config = config;
            MonitorPin = null;
            EnablePin = null;
            _monitorTimer = timer;
            _config = config;
            _monitorTimer.Interval = 1000;
            _monitorTimer.Elapsed += MonitorTimerOnElapsed;

            //_monitorTimer.
            _state = RelayState.None;
        }

        private void MonitorTimerOnElapsed(object sender)
        {
            if (_state == RelayState.Alarm)
                return;
            
            if (_state == RelayState.TryOn)
            {
                if (!GetMonitorState(MonitorPin.State))
                {
                    _state = RelayState.Alarm;
                    OnAlarmNotify(
                        $"Рэле {_config.RelayName} не включилось после подачи команды в течении времени ожидания {_config.MonitorTimeout}");
                }
            }
            _monitorTimer.Stop();
        }
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
            if (_state is RelayState.Alarm or RelayState.On or RelayState.TryOn)
                return;
            
            SetEnablePinState(true);
            _state = RelayState.TryOn;
            _monitorTimer.Interval = _config.StateChangeTimeout;
            _monitorTimer.Start();
        }

        public void Off()
        {
            SetEnablePinState(false);
            _state = RelayState.TryOff;
            
            if (GetMonitorState(MonitorPin.State))
            {
                _monitorTimer.Interval = _config.StateChangeTimeout;
                _monitorTimer.Start();
            }
            else
            {
                _state = RelayState.Off;
            }
        }

        /*public override void InitDevice(IConfigurationItem deviceConfig)
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
        }*/

        private void MonitorPinOnPinStateChanged(DiscretePinStateChangedEventArgs ea)
        {
            ClimaContext.Logger.Debug($"{_config.RelayName} monitor pin:{ea.NewState}");
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

                    OnAlarmNotify($"Рэле {_config.RelayName} отключилось во время работы");
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
                /*if (GetMonitorState(ea.NewState))
                {
                    _state = RelayState.Alarm;
                    OnAlarmNotify($"Рэле {_config.RelayName} включилось в ручном режиме");
                }*/
            }
        }
        private bool GetMonitorState(bool pinState)
        {
            var monitorState = false;
            if (_config.MonitorLevel == ActiveLevel.High)
            {
                if (pinState)
                    monitorState = true;
            }
            else if (_config.MonitorLevel == ActiveLevel.Low)
            {
                if (!pinState)
                    monitorState = true;
            }

            return monitorState;
        }
        private void SetEnablePinState(bool state)
        {
            if (_config.EnableLevel == ActiveLevel.High)
                EnablePin.SetState(state, true);
            else if (_config.EnableLevel == ActiveLevel.Low)
                EnablePin.SetState(!state, true);
        }
        public event EventHandler<AlarmEventArgs> Notify;

        public bool IsAlarm => _state == RelayState.Alarm;

        public bool Reset()
        {
            _state = RelayState.Off;
            return true;
        }

        public string NotifierName => _config.RelayName;

        public string Name
        {
            get => _config.RelayName;
        }

        protected virtual void OnAlarmNotify(string message)
        {
            _state = RelayState.Alarm;
            SetEnablePinState(false);
            var ea = new AlarmEventArgs(message);
            Notify?.Invoke(this, ea);
        }
    }
}