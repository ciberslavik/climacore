using System;

namespace Clima.NetworkServer.Services
{
    public interface IServiceExecutor
    {
        object Execute(string name, object parameters);

        void RegisterHandler(string name, Func<object, object> execute);
    }
}