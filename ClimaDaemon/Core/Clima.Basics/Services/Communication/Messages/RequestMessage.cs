using System.Runtime.Serialization;

namespace Clima.Basics.Services.Communication.Messages
{
    [DataContract]
    public class RequestMessage:IMessage
    {
        [DataMember(Name = "jsonrpc", EmitDefaultValue = true)]
        public string Version => "0.1a";
        [DataMember(Name = "service", EmitDefaultValue = true)]
        public string Service { get; set; }
        
        [DataMember(Name = "method", EmitDefaultValue = true)]
        public string Method { get; set; }

        [DataMember(Name = "params", EmitDefaultValue = true)]
        public object Parameters { get; set; }

        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        [IgnoreDataMember]
        public bool IsNotification => string.IsNullOrWhiteSpace(Id);

        public override string ToString() => $"--> {Method}" +
                                             (Id != null ? $" #{Id}" : string.Empty);

        public bool Equals(RequestMessage other) =>
            other != null &&
            Equals(Version, other.Version) &&
            Equals(Method, other.Method) &&
            Equals(Parameters, other.Parameters) &&
            Equals(Id, other.Id);

        public override bool Equals(object obj) => Equals(obj as RequestMessage);

        public override int GetHashCode() =>
            (Method ?? string.Empty).GetHashCode() ^ (Id ?? string.Empty).GetHashCode(); 
    }
}