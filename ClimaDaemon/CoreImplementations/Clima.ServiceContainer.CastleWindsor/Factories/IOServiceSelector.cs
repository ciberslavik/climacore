using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Castle.Facilities.TypedFactory;
using Clima.AgavaModBusIO;
using Clima.AgavaModBusIO.Model;
using Clima.Core.Tests.IOService;

namespace Clima.ServiceContainer.CastleWindsor.Factories
{
    public class IOServiceSelector:DefaultTypedFactoryComponentSelector
    {
        protected override string GetComponentName(MethodInfo method, object[] arguments)
        {
            if (method.Name == "Create" && arguments.Length == 1 && arguments[0] is bool)
            {
                if ((bool) arguments[0])
                {
                    return typeof(StubIOService).FullName;
                }
                else
                {
                    return typeof(AgavaIoService).FullName;
                }
            }
            return base.GetComponentName(method, arguments);
        }
    }
}