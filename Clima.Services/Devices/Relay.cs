using Clima.Services.Devices.Configs;

namespace Clima.Services.Devices
{
    public class Relay:Device
    {
        private RelayConfig _config;
        public Relay(RelayConfig config)
        {
        }


        public override void InitDevice()
        {
            throw new System.NotImplementedException();
        }
    }
}