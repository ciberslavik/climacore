using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using Clima.Basics.Services.Communication.Messages;

namespace Clima.Communication
{
    public class MessageUnit
    {
        public ConcurrentDictionary<string, Type> RequestTypes { get; } =
            new ConcurrentDictionary<string, Type>();

        public ConcurrentDictionary<string, Type> ResponseTypes { get; } =
            new ConcurrentDictionary<string, Type>();
    }

    public class MessageTypeProvider : IMessageTypeProvider
    {
        public MessageTypeProvider()
        {
        }

        private ConcurrentDictionary<string, MessageUnit> Services { get; } =
            new ConcurrentDictionary<string, MessageUnit>();


        public void Register(string serviceName, string methodName, Type requestType, Type responseType = null)
        {
            if (!Services.ContainsKey(serviceName)) Services.TryAdd(serviceName, new MessageUnit());

            if (Services.TryGetValue(serviceName, out var msgUnit))
            {
                msgUnit.RequestTypes[methodName] = requestType ?? throw new ArgumentNullException(nameof(requestType));
                msgUnit.ResponseTypes[methodName] = responseType;
            }
        }


        public Type TryGetRequestType(string serviceName, string methodName)
        {
            if (Services.TryGetValue(serviceName, out var msgUnit))
                if (msgUnit.RequestTypes.TryGetValue(methodName, out var result))
                    return result;

            return null;
        }

        public Type TryGetResponseType(string serviceName, string methodName)
        {
            if (Services.TryGetValue(serviceName, out var msgUnit))
                if (msgUnit.ResponseTypes.TryGetValue(methodName, out var result))
                    return result;

            return null;
        }

        public virtual string IReturnVoidInterfaceName { get; set; } = "IReturnVoid";

        public virtual string IReturnInterfaceName { get; set; } = "IReturn`1";

        protected virtual Type TryGetResponseType(Type requestType)
        {
            // support JsonService.IReturn and ServiceStack.IReturn
            var retTypes =
                from inter in requestType.GetTypeInfo().GetInterfaces()
                where
                    inter.Name == IReturnVoidInterfaceName && !inter.IsGenericType ||
                    inter.Name == IReturnInterfaceName && inter.IsGenericType
                select inter;

            var retType = retTypes.FirstOrDefault();
            if (retType == null) return null;

            if (retType.Name == IReturnVoidInterfaceName) return typeof(void);

            if (retType.IsGenericType) return retType.GetGenericArguments().Single();

            return null;
        }
    }
}