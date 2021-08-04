using System.Runtime.Serialization;

namespace Clima.Basics.Services.Communication.Messages
{
    [DataContract]
    public class ResponseResultMessage:ResponseMessage
    {
        [DataMember(Name = "jsonrpc", EmitDefaultValue = true)]
        public override string Version => "0.1a";
        
        [DataMember(Name = "service",EmitDefaultValue = true)]
        public override string Service { get; set; }
        
        [DataMember(Name = "method",EmitDefaultValue = true)]
        public override string MethodName { get; set; }

        [DataMember(Name = "result", EmitDefaultValue = true)]
        public override object Result { get; set; }

        [IgnoreDataMember]
        public override Error Error { get; set; }
    }
}