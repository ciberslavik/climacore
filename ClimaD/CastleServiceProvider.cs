using Castle.Windsor;
using Clima.Services;

namespace ClimaD
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
    }
}