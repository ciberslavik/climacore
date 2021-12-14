using Clima.Basics.Services.Communication.Messages;

namespace Clima.NetworkServer
{
    public interface INetworkSerializer
    {
        string Serialize(IMessage message);
        IMessage Deserialize(string data, IMessageTypeProvider typeProvider, IMessageNameProvider nameProvider);
    }
}