namespace Clima.NetworkServer.Services
{
    public interface IMessageNameProvider
    {
        string TryGetMessageName(string messageId);
    }
}