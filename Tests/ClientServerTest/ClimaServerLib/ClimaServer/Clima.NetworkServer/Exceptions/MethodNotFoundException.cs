using System;

namespace Clima.NetworkServer.Exceptions
{
    public class MethodNotFoundException:Exception
    {
        public MethodNotFoundException(string methodName="")
        {
        }

        
    }
}