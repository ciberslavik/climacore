using System;

namespace Clima.Services.IO
{
    public class IOServiceException:Exception
    {
        public IOServiceException(string message=""):base(message)
        {
            
        }
        
    }
}