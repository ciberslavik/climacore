namespace Clima.Core.Devices
{
    public interface IDeviceProvider
    {
        IRelay GetRelay(string relayName);
    }
}