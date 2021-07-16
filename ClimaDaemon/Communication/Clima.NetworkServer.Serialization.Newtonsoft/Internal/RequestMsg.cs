using System.Runtime.Serialization;

namespace Clima.NetworkServer.Serialization.Newtonsoft.Internal
{
    [DataContract]
    internal class RequestMsg<T>:IRequestMessage
    {
        [DataMember(Name = "params")]
        public T Parameters { get; set; }
        object IRequestMessage.Parameters => Parameters; 
    }
}