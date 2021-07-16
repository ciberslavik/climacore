using System;

namespace Clima.Basics.Services.Communication.Messages
{
    public interface IMessageTypeProvider
    {
        void Register(string name, Type requestType, Type responseType = null);
        Type TryGetRequestType(string name);
        Type TryGetResponseType(string name);
    }
}