using System;

namespace Clima.NetworkServer.Services
{
    public interface IServiceExecutor
    {
        object Execute(string method, object parameters);
        object Execute(string service, string method, object parameters);
        
        void RegisterHandler(string method, Func<object, object> execute);
        void RegisterHandler(string service, string method, Func<object, object> execute);
    }
}