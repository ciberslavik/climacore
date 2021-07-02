using Clima.DataModel.Configurations.IOSystem;
using Clima.Services.Devices.Alarm;
using Clima.Services.IO;

namespace Clima.Services.Devices
{
    public abstract class Device
    {
        
        public string Name { get; set; }

        protected Device()
        {
        }

        
        public abstract void InitDevice(DeviceConfigBase deviceConfig);
        
    }
}