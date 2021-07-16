using System.Runtime.Serialization;
using Clima.Basics.Services.Communication.Messages;

namespace Clima.NetworkServer.Messages
{
    [DataContract]
    public class ResponseResultMessage:ResponseMessage
    {
        [DataMember(Name = "jsonrpc", EmitDefaultValue = true)]
        public override string Version => "2.0";
        
        [DataMember(Name = "service",EmitDefaultValue = true)]
        public override string Service { get; set; }

        [DataMember(Name = "result", EmitDefaultValue = true)]
        public override object Result { get; set; }

        [IgnoreDataMember]
        public override Error Error { get; set; }
    }
}