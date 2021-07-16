using System;
using Castle.Windsor;
using Clima.Basics.Services;
using IServiceProvider = Clima.Basics.Services.IServiceProvider;
namespace Clima.ServiceContainer.CastleWindsor
{
    public class CastleServiceProvider:IServiceProvider
    {
        private readonly IWindsorContainer _container;

        public CastleServiceProvider(IWindsorContainer container)
        {
            _container = container;
        }
        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public void InitializeService(IServiceInitializer initializer)
        {
            initializer.Initialize(this);
        }
    }
}