using System.Runtime.Serialization;
using Clima.NetworkServer.Messages;

namespace Clima.Basics.Services.Communication.Messages
{
    [DataContract]
    public class ResponseErrorMessage:ResponseMessage
    {
        [DataMember(Name = "jsonrpc", EmitDefaultValue = true)]
        public override string Version => "0.1a";
        
        [DataMember(Name = "service", EmitDefaultValue = true)]
        public override string Service { get; set; }

        [DataMember(Name = "method", EmitDefaultValue = true)]
        public override string MethodName { get; set; }
        

        [IgnoreDataMember]
        public override object Result { get; set; }

        [DataMember(Name = "error", EmitDefaultValue = true)]
        public override Error Error { get; set; }   
    }
}