namespace Clima.Basics.Services.Communication.Messages
{
    public interface IMessageNameProvider
    {
        string TryGetMessageName(string messageId);
    }
}