using System;
using System.Linq;
using Clima.Core.Controllers.Light;
using Clima.Core.Devices;

namespace Clima.Core.Conrollers.Ventilation
{
    public class LightController:ILightController
    {
        public LightController()
        {
        }
        public LightState State { get; private set; }
        public bool IsManual { get; set; }
        public LightTimerPreset Preset { get; set; }
        public void Process(int currentDay)
        {
            if(IsManual)
                return;
            
            Preset.Days.Sort();

            var currentConfigDay = Preset.Days.Last(t => t.Day <= currentDay);

            foreach (var timer in currentConfigDay.Timers)
            {
                if (timer.ContainsTime(DateTime.Now))
                {
                    LightRelay.On();
                    State = LightState.LightOn;
                    return;
                }
            }
            
            LightRelay.Off();
            State = LightState.LightOff;
        }

        public void ManualOn()
        {
            if(IsManual)
                LightRelay.On();
        }

        public void ManualOff()
        {
            if(IsManual)
                LightRelay.Off();
        }

        internal IRelay LightRelay { get; set; }
        
    }
}