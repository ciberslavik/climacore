using System.Runtime.Serialization;
using Clima.NetworkServer.Messages;

namespace Clima.Basics.Services.Communication.Messages
{
    [DataContract]
    public class ResponseErrorMessage:ResponseMessage
    {
        [DataMember(Name = "jsonrpc", EmitDefaultValue = true)]
        public override string Version => "2.0";

        [IgnoreDataMember]
        public override object Result { get; set; }

        [DataMember(Name = "error", EmitDefaultValue = true)]
        public override Error Error { get; set; }   
    }
}