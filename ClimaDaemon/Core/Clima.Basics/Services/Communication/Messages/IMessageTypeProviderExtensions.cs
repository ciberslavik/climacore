using System;
using Clima.Basics.Services.Communication.Exceptions;

namespace Clima.Basics.Services.Communication.Messages
{
    public static class IMessageTypeProviderExtensions
    {
        public static Type GetRequestType(this IMessageTypeProvider self, string serviceName, string methodName)
        {
            var result = self.TryGetRequestType(serviceName, methodName);
            if (result != null)
            {
                return result;
            }

            throw new MethodNotFoundException(serviceName, methodName);
        }
        public static Type GetResponseType(this IMessageTypeProvider self, string serviceName, string methodName)
        {
            var result = self.TryGetResponseType(serviceName, methodName);
            if (result != null)
            {
                return result;
            }

            throw new MethodNotFoundException(serviceName, methodName);
        }
    }
    
}