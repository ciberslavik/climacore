using Clima.Services.Alarm;
using Clima.Services.Devices;

namespace Clima.Core.Ventelation
{
    public class DiscreteFan:IDiscreteFan, IAlarmNotifier
    {
        private readonly DiscreteFanConfig _config;
        private readonly Relay _fanRelay;
        
        public DiscreteFan(Relay relay)
        {
            _fanRelay = relay;
        }

        public void Start()
        {
            _fanRelay.On();
            IsRunning = true;
        }

        public void Stop()
        {
            
        }
        public bool IsRunning { get; private set; }

        public DiscreteFanConfig Config => _config;

        public bool IsAlarm { get; set; }
        public event AlarmNotifyHandler AlarmNotify;

        protected virtual void OnAlarmNotify(AlarmNotifyEventArgs ea)
        {
            AlarmNotify?.Invoke(ea);
        }
    }
}