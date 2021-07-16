using Clima.Basics.Services.Communication.Messages;

namespace Clima.Basics.Services.Communication
{
    public interface INetworkSerializer
    {
        string Serialize(IMessage message);
        IMessage Deserialize(string data,IMessageTypeProvider typeProvider,IMessageNameProvider nameProvider);
    }
}