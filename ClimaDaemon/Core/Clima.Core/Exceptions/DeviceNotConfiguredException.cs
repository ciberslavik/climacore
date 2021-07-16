using System;

namespace Clima.Core.Exceptions
{
    public class DeviceNotConfiguredException:Exception
    {
        public DeviceNotConfiguredException()
        {
        }

        public DeviceNotConfiguredException(string message) : base(message)
        {
            
        }
        
    }
}