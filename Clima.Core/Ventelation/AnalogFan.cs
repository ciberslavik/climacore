using Clima.Services.Alarm;
using Clima.Services.Devices;

namespace Clima.Core.Ventelation
{
    public class AnalogFan:IAnalogFan, IAlarmNotifier
    {
        private readonly FrequencyConverter _fc;
        private double _value;
        private AnalogFanConfig _config;

        public AnalogFan(FrequencyConverter fc)
        {
            _fc = fc;
        }

        public void Start()
        {
            
        }
        public void Stop()
        {
            
        }

        public void SetValue(double value)
        {
            throw new System.NotImplementedException();
        }

        public double Value => _value;

        public bool IsRunning { get; set; }

        public AnalogFanConfig Config => _config;

        public event AlarmNotifyHandler AlarmNotify;

        protected virtual void OnAlarmNotify(AlarmNotifyEventArgs ea)
        {
            AlarmNotify?.Invoke(ea);
        }
    }
}