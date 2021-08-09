using Clima.Basics.Configuration;

namespace Clima.Core.Devices
{
    public abstract class Device
    {
        public string Name { get; set; }

        protected Device()
        {
        }


        public abstract void InitDevice(IConfigurationItem deviceConfig);
    }
}