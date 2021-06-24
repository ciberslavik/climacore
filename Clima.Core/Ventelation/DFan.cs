using Clima.Core.Alarm;
using Clima.Services.Devices;

namespace Clima.Core.Ventelation
{
    public class DFan:IAlarmNotifier
    {
        private readonly DFanConfig _config;
        private readonly Relay _fanRelay;

        public DFan(DFanConfig config, Relay fanRelay)
        {
            _config = config;
            _fanRelay = fanRelay;
        }

        public void Start()
        {
            
        }

        public void Stop()
        {
            
        }
        public bool IsRunning { get; set; }
        public bool IsAlarm { get; set; }
        public event AlarmNotifyHandler AlarmNotify;

        protected virtual void OnAlarmNotify(AlarmNotifyEventArgs ea)
        {
            AlarmNotify?.Invoke(ea);
        }
    }
}