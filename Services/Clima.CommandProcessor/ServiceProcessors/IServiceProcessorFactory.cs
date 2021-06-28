namespace Clima.CommandProcessor.ServiceProcessors
{
    public interface IServiceProcessorFactory
    {
        IAuthorizationProcessor CreateAuthorizationProcessor();
    }
}