namespace Clima.Core.Devices
{
    public interface IFrequencyConverter
    {
        void Start();
        void Stop();
        void SetPower(float power);
        float Power { get; }
    }
}