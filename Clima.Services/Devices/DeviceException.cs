using System;

namespace Clima.Services.Devices
{
    public class DeviceException:Exception
    {
        public DeviceException(string message = ""):base(message)
        {
            
        }
    }
}