using Clima.Basics.Services;

namespace Clima.Core.IO
{
    public interface IIOService
    {
        bool IsInit { get; }
        bool IsRunning { get; }
        void Init(object config);
        void Start();
        void Stop();
        void Read();
        void Write();
        IOPinCollection Pins { get; }
    }
}