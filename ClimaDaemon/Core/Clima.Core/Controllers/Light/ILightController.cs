namespace Clima.Core.Controllers.Light
{
    public interface ILightController
    {
        LightState State { get; }
        LightTimerPreset Preset { get; set; }
        void Process(int currentDay);
        void ManualOn();
        void ManualOff();
        bool IsManual { get; set; }
    }
}