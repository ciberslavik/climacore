using System;
using System.Collections.Concurrent;
using Clima.Basics.Services.Communication.Exceptions;
using Clima.NetworkServer.Services;

namespace Clima.Communication
{
    public class ServiceExecutor:IServiceExecutor
    {                           //ServiceName                MethodName     MethodInvoker
        private ConcurrentDictionary<string, ConcurrentDictionary<string, Func<object, object>>> RegisteredHandlers { get; } =
            new ConcurrentDictionary<string, ConcurrentDictionary<string, Func<object, object>>>();

        public object Execute(string service, string method, object parameters)
        {
            
            throw new MethodNotFoundException(method);
        }
        public object Execute(string method, object parameters)
        {
            // execute the requested service
            if (RegisteredHandlers.TryGetValue("", out var service))
            {
                if(service.TryGetValue(method, out var handler))
                    return handler(parameters);
            }

            throw new MethodNotFoundException(method);
        }

        public void RegisterHandler(string name, Func<object, object> execute)
        {
            RegisteredHandlers[name ?? throw new ArgumentNullException(nameof(name))] =
                execute ?? throw new ArgumentNullException(nameof(execute));
        }
    }
}