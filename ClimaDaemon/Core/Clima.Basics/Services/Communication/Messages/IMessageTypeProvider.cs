using System;

namespace Clima.Basics.Services.Communication.Messages
{
    public interface IMessageTypeProvider
    {
        void Register(string serviceName, string methodName, Type requestType, Type responseType = null);
        Type TryGetRequestType(string serviceName, string methodName);
        Type TryGetResponseType(string serviceName, string methodName);
    }
}