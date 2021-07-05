using System;
using System.Collections.Concurrent;
using Clima.NetworkServer.Exceptions;

namespace Clima.NetworkServer.Services
{
    public class ServiceExecutor:IServiceExecutor
    {
        private ConcurrentDictionary<string, Func<object, object>> RegisteredHandlers { get; } =
            new ConcurrentDictionary<string, Func<object, object>>();
        
        public object Execute(string name, object parameters)
        {
            // execute the requested service
            if (RegisteredHandlers.TryGetValue(name, out var handler))
            {
                return handler(parameters);
            }

            throw new MethodNotFoundException(name);
        }

        public void RegisterHandler(string name, Func<object, object> execute)
        {
            RegisteredHandlers[name ?? throw new ArgumentNullException(nameof(name))] =
                execute ?? throw new ArgumentNullException(nameof(execute));
        }
    }
}