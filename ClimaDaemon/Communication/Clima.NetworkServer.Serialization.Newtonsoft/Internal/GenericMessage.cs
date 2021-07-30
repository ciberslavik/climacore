using System.Runtime.Serialization;
using Clima.Basics.Services.Communication.Messages;
using Clima.NetworkServer.Messages;

namespace Clima.NetworkServer.Serialization.Newtonsoft.Internal
{
    [DataContract]
    public class GenericMessage
    {
        [DataMember(Name = "jsonrpc")]
        public string Version { get; set; }

        [DataMember(Name = "service")]
        public string Name { get; set; }
        
        [DataMember(Name = "method")]
        public string Method { get; set; }

        [DataMember(Name = "error")]
        public Error Error { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        public bool IsValid => Version == "2.0" &&
                               (!string.IsNullOrWhiteSpace(Name) || !string.IsNullOrWhiteSpace(Id)); 
    }
}