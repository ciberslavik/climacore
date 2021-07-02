using System;

namespace Clima.Services.Devices.Exceptions
{
    public class DevicePinsNotConfiguredException:Exception
    {
        public DevicePinsNotConfiguredException(string message=""):base(message)
        {
            
        }
    }
}