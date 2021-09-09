namespace Clima.Core.Devices
{
    public interface IHeater
    {
        void On();
        void Off();
        bool IsOn { get; }
    }
}