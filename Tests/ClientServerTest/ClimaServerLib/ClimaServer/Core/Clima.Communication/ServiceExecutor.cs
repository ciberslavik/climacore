using System;
using System.Collections.Concurrent;
using Clima.Basics.Services.Communication.Exceptions;
using Clima.NetworkServer.Services;
using IServiceProvider = Clima.Basics.Services.IServiceProvider;

namespace Clima.Communication
{
    public class ServiceExecutor:IServiceExecutor
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceExecutor(IServiceProvider _serviceProvider)
        {
            this._serviceProvider = _serviceProvider;
        } 
                                   //ServiceName                MethodName     MethodInvoker
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

        public void RegisterHandler(string method, Func<object, object> execute)
        {
            RegisteredHandlers[""][method ?? throw new ArgumentNullException(nameof(method))] =
                execute ?? throw new ArgumentNullException(nameof(execute));
        }
        
        public void RegisterHandler(string service,string method, Func<object, object> execute)
        {
            RegisteredHandlers[service ?? throw new ArgumentNullException(nameof(service))]
                    [method ?? throw new ArgumentNullException(nameof(method))] =
                execute ?? throw new ArgumentNullException(nameof(execute));
        }
    }
}