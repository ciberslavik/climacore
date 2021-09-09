using Clima.Core.IO;

namespace Clima.Core.Devices
{
    public class Heater:IHeater
    {
        public Heater()
        {
        }
        public IDiscreteOutput EnablePin { get; set; }

        public void On()
        {
            if (EnablePin is not null)
            {
                EnablePin.SetState(true);
            }
        }

        public void Off()
        {
            if (EnablePin is not null)
            {
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
    }
}