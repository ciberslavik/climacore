using System.IO;
using Clima.NetworkServer.Messages;
using Clima.NetworkServer.Services;
using Newtonsoft.Json;

namespace Clima.NetworkServer.Serialization.Newtonsoft
{
    public class Serializer:INetworkSerializer
    {
        private JsonSerializer JsonSerializer { get; set; } = JsonSerializer.Create();
        
        public string Serialize(IMessage message)
        {
            using (var sw = new StringWriter())
            {
                JsonSerializer.Serialize(sw, message);
                return sw.ToString();
            }  
        }

        public IMessage Deserialize(string data, IMessageTypeProvider typeProvider, IMessageNameProvider nameProvider)
        {
            throw new System.NotImplementedException();
        }
    }
}