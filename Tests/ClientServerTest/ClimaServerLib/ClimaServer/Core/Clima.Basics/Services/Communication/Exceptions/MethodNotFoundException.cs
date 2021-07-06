using System;

namespace Clima.Basics.Services.Communication.Exceptions
{
    public class MethodNotFoundException:Exception
    {
        public MethodNotFoundException(string methodName="")
        {
            Console.WriteLine($"Method not found:{methodName}");
        }

        
    }
}