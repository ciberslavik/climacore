using System;

namespace Clima.NetworkServer.Services
{
    public interface IMessageTypeProvider
    {
        void Register(string name, Type requestType, Type responseType = null);
        Type TryGetRequestType(string name);
        Type TryGetResponseType(string name);
    }
}