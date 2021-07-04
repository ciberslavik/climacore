using Clima.NetworkServer.Messages;
using Clima.NetworkServer.Services;

namespace Clima.NetworkServer.Serialization
{
    public interface INetworkSerializer
    {
        string Serialize(IMessage message);
        IMessage Deserialize(string data,IMessageTypeProvider typeProvider,IMessageNameProvider nameProvider);
    }
}