using Clima.Services.Devices.Alarm;
using Clima.Services.IO;

namespace Clima.Services.Devices
{
    public abstract class Device
    {
        private DeviceState _deviceState;
        public string Name { get; set; }

        protected Device()
        {
            _deviceState = DeviceState.Unknown;
        }

        public event DeviceAlarmEventHandler Alarm;

        public virtual DeviceState State
        {
            get => _deviceState;
            protected set => _deviceState = value;
        }

        public abstract void InitDevice();

        protected virtual void OnAlarm(DeviceAlarmEventArgs ea)
        {
            Alarm?.Invoke(ea);
        }
    }
}