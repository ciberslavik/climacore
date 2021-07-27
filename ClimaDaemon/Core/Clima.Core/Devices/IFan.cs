namespace Clima.Core.Devices
{
    public interface IFan
    {
        void Start();
        void Stop();
        FanState State { get; }
    }
}