namespace Clima.Core.Devices
{
    public class CoreDeviceProvider:IDeviceProvider
    {
        public CoreDeviceProvider()
        {
        }


        public IRelay GetRelay(string relayName)
        {
            throw new System.NotImplementedException();
        }
    }
}