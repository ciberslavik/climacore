namespace Clima.Core.Devices
{
    public interface IHeater
    {
        string HeaterName { get; set; }
        void On();
        void Off();
        bool IsOn { get; }
    }
}