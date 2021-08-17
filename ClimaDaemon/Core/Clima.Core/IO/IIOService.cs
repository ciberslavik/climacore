using Clima.Basics.Services;

namespace Clima.Core.IO
{
    public interface IIOService : IService
    {
        bool IsInit { get; }
        bool IsRunning { get; }

        IOPinCollection Pins { get; }
    }
}