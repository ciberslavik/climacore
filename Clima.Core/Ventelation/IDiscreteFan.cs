namespace Clima.Core.Ventelation
{
    public interface IDiscreteFan
    {
        void Start();
        void Stop();
        bool IsRunning { get; }
        DiscreteFanConfig Config { get; }
    }
}