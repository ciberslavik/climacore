using System.Timers;
using Clima.Services.Alarm;
using Clima.Services.Devices.Configs;
using Clima.Services.IO;

namespace Clima.Services.Devices
{
    public class Relay:Device, IAlarmNotifier
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
        private RelayConfig _config;
        private DiscreteOutput _enablePin;
        private DiscreteInput _monitorPin;
        private ITimer _monitorTimer;

        private RelayState _state;
        public Relay(ITimer timer)
        {
            MonitorPin = null;
            EnablePin = null;
            _config = RelayConfig.CreateDefaultConfig();
            _monitorTimer = timer;
            _monitorTimer.Elapsed += MonitorTimerOnElapsed;
            
            //_monitorTimer.
            _state = RelayState.None;
        }

        private void MonitorTimerOnElapsed(object sender)
        {
            
            if (_state == RelayState.TryOff)
            {
                if (GetMonitorState())
                {
                    _state = RelayState.Alarm;
                    throw new DeviceException($"Рэле {_config.RelayName} не отключилось в течении времени ожидания");
                }
                _state = RelayState.Off;
            }
            else if (_state == RelayState.TryOn)
            {
                if (!GetMonitorState())
                {
                        _state = RelayState.Alarm;
                        throw new DeviceException($"Рэле {_config.RelayName} не включилось после подачи команды в течении времени ожидания {_config.MonitorTimeout}");
                }
                _state = RelayState.Off;
            }
        }

        public RelayConfig Configuration
        {
            get => _config;
            set => _config = value;
        }
        public DiscreteInput MonitorPin { get; set; }
        public DiscreteOutput EnablePin { get; set; }
        
        public void On()
        {
            if (_config.EnableLevel == ActiveLevel.High)
            {
                if (!EnablePin.State)
                {
                    EnablePin.State = true;
                }
            }
            else if(_config.EnableLevel == ActiveLevel.Low)
            {
                if (EnablePin.State)
                {
                    EnablePin.State = false;
                }
            }

            _state = RelayState.TryOn;
            _monitorTimer.Start();
        }
        public void Off()
        {
            if (_config.EnableLevel == ActiveLevel.High)
            {
                if (EnablePin.State)
                {
                    EnablePin.State = false;
                }
            }
            else if(_config.EnableLevel == ActiveLevel.Low)
            {
                if (!EnablePin.State)
                {
                    EnablePin.State = true;
                }
            }

            _state = RelayState.TryOff;
            
            _monitorTimer.Start();
        }
        public override void InitDevice()
        {
            if(MonitorPin == null || EnablePin == null)
                return;
            _state = RelayState.Init;
            MonitorPin.PinStateChanged += MonitorPinOnPinStateChanged;
            _monitorTimer.Interval = _config.MonitorTimeout;
            Off();
        }

        private void MonitorPinOnPinStateChanged(DiscretePinStateChangedEventArgs ea)
        {
            if (_state == RelayState.TryOn)
            {
                if (GetMonitorState())
                {
                    _monitorTimer.Stop();
                    _state = RelayState.On;
                }
            }
            else if (_state == RelayState.On)
            {
                if (!GetMonitorState())
                {
                    _state = RelayState.Alarm;
                    throw new DeviceException($"Рэле {_config.RelayName} отключилось во время работы");
                }
            }
            else if (_state == RelayState.TryOff)
            {
                if (!GetMonitorState())
                {
                    _state = RelayState.Off;
                    _monitorTimer.Stop();
                }
            }
            else if (_state == RelayState.Off)
            {
                if (GetMonitorState())
                {
                    _state = RelayState.Alarm;
                    throw new DeviceException($"Рэле {_config.RelayName} включилось в ручном режиме");
                }
            }
        }

        private bool GetMonitorState()
        {
            bool monitorState = false;
            if (_config.MonitorLevel == ActiveLevel.High)
            {
                if (MonitorPin.State)
                    monitorState = true;
            }
            else if (_config.MonitorLevel == ActiveLevel.Low)
            {
                if (!MonitorPin.State)
                    monitorState = true;
            }

            return monitorState;
        }

        private void SetEnableState(bool state)
        {
            if (_config.EnableLevel == ActiveLevel.High)
                EnablePin.State = state;
            else if (_config.EnableLevel == ActiveLevel.Low)
                EnablePin.State = !state;
        }
        public event AlarmNotifyHandler AlarmNotify;

        protected virtual void OnAlarmNotify(AlarmNotifyEventArgs ea)
        {
            AlarmNotify?.Invoke(ea);
        }
    }
}