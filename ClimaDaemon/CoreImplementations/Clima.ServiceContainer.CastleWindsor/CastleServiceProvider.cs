using System;
using Castle.MicroKernel.Registration;
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

        public void Register<TService, TImpl>(string name = "") 
            where TService:class
            where TImpl: TService
        {
            if (name == "")
                _container.Register(
                    Component
                        .For<TService>()
                        .ImplementedBy<TImpl>()
                        .LifestyleSingleton());
            else
                _container.Register(
                    Component
                        .For<TService>()
                        .ImplementedBy<TImpl>()
                        .Named(name)
                        .LifestyleSingleton());

        }

        public void InitializeService(IServiceInitializer initializer)
        {
            initializer.Initialize(this);
        }
    }
}