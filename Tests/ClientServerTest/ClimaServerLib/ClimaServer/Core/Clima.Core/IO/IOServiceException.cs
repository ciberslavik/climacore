using System;

namespace Clima.Core.IO
{
    public class IOServiceException:Exception
    {
        public IOServiceException(string message=""):base(message)
        {
            
        }
        
    }
}