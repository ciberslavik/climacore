using Clima.Basics.Services.Communication;
using Clima.Basics.Services.Communication.Messages;

namespace Clima.Serialization.Newtonsoft
{
    public class NetworkSerializer:INetworkSerializer
    {
        public NetworkSerializer()
        {
        }


        public string Serialize(IMessage message)
        {
            throw new System.NotImplementedException();
        }

        public IMessage Deserialize(string data, IMessageTypeProvider typeProvider, IMessageNameProvider nameProvider)
        {
            throw new System.NotImplementedException();
        }
    }
}