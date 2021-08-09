using System;
using System.Linq;
using Clima.Basics.Services.Communication.Exceptions;
using Clima.Basics.Services.Communication.Messages;

namespace Clima.Basics.Services.Communication
{
    public class DefaultServiceRegistrator : INetworkServiceRegistrator
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageTypeProvider _messageTypeProvider;
        private readonly IServiceExecutor _serviceExecutor;

        public DefaultServiceRegistrator(
            IServiceProvider serviceProvider,
            IMessageTypeProvider messageTypeProvider,
            IServiceExecutor serviceExecutor)
        {
            _serviceProvider = serviceProvider;
            _messageTypeProvider = messageTypeProvider;
            _serviceExecutor = serviceExecutor;
        }

        public void RegisterNetworkService<TService>() where TService : INetworkService
        {
            var serviceType = typeof(TService);

            foreach (var methodInfo in serviceType.GetMethods())
                if (methodInfo.GetCustomAttributes(typeof(ServiceMethodAttribute), false).Any())
                {
                    var responseType = methodInfo.ReturnType;
                    var requestType = methodInfo.GetParameters().FirstOrDefault()?.ParameterType;

                    if (requestType is not null)
                    {
                        _messageTypeProvider.Register(
                            serviceType.Name,
                            methodInfo.Name,
                            requestType,
                            responseType);

                        var registerMethod = _serviceProvider.GetType()
                            .GetMethod(nameof(_serviceProvider.RegisterWithoutInterface));
                        if (registerMethod is not null) registerMethod.MakeGenericMethod(serviceType);


                        _serviceExecutor.RegisterHandler(serviceType.Name, methodInfo.Name, (p) =>
                        {
                            var resolveMethod = _serviceProvider.GetType().GetMethod(nameof(_serviceProvider.Resolve));

                            if (resolveMethod is not null)
                            {
                                resolveMethod = resolveMethod.MakeGenericMethod(serviceType);
                                var service = resolveMethod.Invoke(_serviceProvider, null);
                                if (!(service is null)) return methodInfo.Invoke(service, new[] {p});
                            }

                            throw new InvalidRequestException(
                                $"error execute service:{serviceType.Name} method:{methodInfo.Name}");
                        });
                    }
                }
        }
    }
}