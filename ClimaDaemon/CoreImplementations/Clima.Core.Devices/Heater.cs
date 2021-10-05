using System;
using Clima.Basics.Services;
using Clima.Core.IO;

namespace Clima.Core.Devices
{
    public class Heater:IHeater
    {
        public Heater()
        {
        }
        public IDiscreteOutput EnablePin { get; set; }
        
        public string HeaterName { get; set; }

        public void On()
        {
            if (EnablePin is not null)
            {
                Log.Debug($"Heater:{HeaterName} On");
                EnablePin.SetState(true);
            }
        }

        public void Off()
        {
            if (EnablePin is not null)
            {
                Log.Debug($"Heater:{HeaterName} Off");
                EnablePin.SetState(false);
            }
        }

        public bool IsOn
        {
            get
            {
                if (EnablePin is not null)
                {
                    return EnablePin.State;
                }

                return false;
            }
        }
        internal ISystemLogger Log { get; set; }
    }
}