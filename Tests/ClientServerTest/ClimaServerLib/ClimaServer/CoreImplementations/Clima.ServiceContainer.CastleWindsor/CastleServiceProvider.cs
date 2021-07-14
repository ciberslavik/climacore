using System;
using Castle.Windsor;
using Clima.Basics.Services;
using IServiceProvider = Clima.Basics.Services.IServiceProvider;
namespace Clima.ServiceContainer.CastleWindsor
{
    public class CastleServiceProvider:IServiceProvider
    {
        private IWindsorContainer _container;

        public CastleServiceProvider()
        {
            _container = new WindsorContainer();
        }
        public T Resolve<T>()
        {
            throw new NotImplementedException();
        }

        public void InitializeService(IServiceInitializer initializer)
        {
            initializer.Initialize(this);
        }
    }
}