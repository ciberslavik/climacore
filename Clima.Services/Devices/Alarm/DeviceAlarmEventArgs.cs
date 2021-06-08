using System;

namespace Clima.Services.Devices.Alarm
{
    public delegate void DeviceAlarmEventHandler(DeviceAlarmEventArgs ea);
    public class DeviceAlarmEventArgs:EventArgs
    {
        public DeviceAlarmEventArgs()
        {
        }
    }
}