using System;
using System.Collections.Generic;
using Clima.Basics.Services;
using Clima.Core.Alarm;

namespace Clima.Alarms
{
    public class AlarmManager:IAlarmManager
    {
        private Dictionary<string, IAlarmNotifier> _notifiers;
        public ISystemLogger Log { get; set; }
        
        public AlarmManager()
        {
            _notifiers = new Dictionary<string, IAlarmNotifier>();
        }
        public void RegisterNotifier(IAlarmNotifier notifier)
        {
            var name = notifier.GetType().Name;
            if (!_notifiers.ContainsKey(name))
            {
                Log.Debug("Added notifier");
                _notifiers.Add(name, notifier);
            }
        }
    }
}