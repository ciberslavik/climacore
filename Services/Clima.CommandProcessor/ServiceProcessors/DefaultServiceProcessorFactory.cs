using Clima.Services;

namespace Clima.CommandProcessor.ServiceProcessors
{
    public class DefaultServiceProcessorFactory:IServiceProcessorFactory
    {
        private readonly IServiceProvider _provider;

        public DefaultServiceProcessorFactory(IServiceProvider _provider)
        {
            this._provider = _provider;
        }


        public IAuthorizationProcessor CreateAuthorizationProcessor()
        {
            throw new System.NotImplementedException();
        }
    }
}