using System;

namespace Clima.Basics.Services.Communication
{
    public interface IServiceExecutor
    {
        object Execute(string methodName, object parameters);
        object Execute(string serviceName, string methodName, object parameters);

        void RegisterHandler(string method, Func<object, object> execute);
        void RegisterHandler(string service, string method, Func<object, object> execute);
    }
}