using System;
using System.Collections.Concurrent;
using Clima.Basics.Services.Communication;
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

        public object Execute(string serviceName, string methodName, object parameters)
        {
            //try getting service
            if (RegisteredHandlers.TryGetValue(serviceName, out var service))
            {
                //try getting method
                if(service.TryGetValue(methodName, out var handler))
                    return handler(parameters);
            }

            throw new MethodNotFoundException(methodName);
        }
        public object Execute(string method, object parameters)
        {
            // execute the requested service
            if (RegisteredHandlers.TryGetValue("root", out var service))
            {
                if(service.TryGetValue(method, out var handler))
                    return handler(parameters);
            }

            throw new MethodNotFoundException(method);
        }

        public void RegisterHandler(string method, Func<object, object> execute)
        {
            if (!RegisteredHandlers.ContainsKey("root"))
                RegisteredHandlers.TryAdd("root", new ConcurrentDictionary<string, Func<object, object>>());
            
            RegisteredHandlers["root"][method ?? throw new ArgumentNullException(nameof(method))] =
                execute ?? throw new ArgumentNullException(nameof(execute));
            
            Console.WriteLine($"Registered service: root, method: {method}");
        }
        
        public void RegisterHandler(string service, string method, Func<object, object> execute)
        {
            if (string.IsNullOrEmpty(service))
                throw new ArgumentNullException(nameof(service));
            
            if (!RegisteredHandlers.ContainsKey(service))
                RegisteredHandlers.TryAdd(service, new ConcurrentDictionary<string, Func<object, object>>());
            
            RegisteredHandlers[service][method ?? throw new ArgumentNullException(nameof(method))] =
                execute ?? throw new ArgumentNullException(nameof(execute));
            
            Console.WriteLine($"Registered network service: {service}, method: {method}");
        }
    }
}