using Clima.Core.Alarm;
using Clima.Services.Devices.Configs;
using Clima.Services.IO;

namespace Clima.Services.Devices
{
    public class Relay:Device, IAlarmNotifier
    {
        private RelayConfig _config;
        private DiscreteOutput _enablePin;
        private DiscreteInput _monitorPin;
        
        public Relay()
        {
        }

        public DiscreteInput MonitorPin { get; private set; }
        public void On()
        {
            if (_config.RelayEdge == ActiveEdge.Rising)
            {
                if (!_enablePin.State)
                {
                    _enablePin.State = true;
                }
            }
            else if(_config.RelayEdge == ActiveEdge.Falling)
            {
                if (_enablePin.State)
                {
                    _enablePin.State = false;
                }
            }
        }
        public void Off()
        {
            if (_config.RelayEdge == ActiveEdge.Rising)
            {
                if (_enablePin.State)
                {
                    _enablePin.State = false;
                }
            }
            else if(_config.RelayEdge == ActiveEdge.Falling)
            {
                if (!_enablePin.State)
                {
                    _enablePin.State = true;
                }
            }
        }
        public override void InitDevice(IIOService ioService, object deviceConfig)
        {
            if (deviceConfig is RelayConfig cfg)
            {
                _enablePin = ioService.Pins.DiscreteOutputs[cfg.RelayPinName];
                _monitorPin = ioService.Pins.DiscreteInputs[cfg.MonitorPinName];
                _monitorPin.PinStateChanged += MonitorPinOnPinStateChanged;
                _config = cfg;
            }
            else
            {
                throw new ConfigNotSupportException(typeof(RelayConfig),deviceConfig.GetType());
            }
        }

        private void MonitorPinOnPinStateChanged(DiscretePinStateChangedEventArgs args)
        {
            throw new System.NotImplementedException();
        }

        public event AlarmNotifyHandler AlarmNotify;

        protected virtual void OnAlarmNotify(AlarmNotifyEventArgs ea)
        {
            AlarmNotify?.Invoke(ea);
        }
    }
}