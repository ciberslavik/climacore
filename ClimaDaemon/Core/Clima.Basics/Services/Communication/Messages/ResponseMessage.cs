using System.Runtime.Serialization;

namespace Clima.Basics.Services.Communication.Messages
{
    [DataContract]
    public abstract class ResponseMessage : IMessage
    {
        public abstract string Version { get; }
        public abstract string Service { get; set; }
        public abstract string MethodName { get; set; }
        public abstract object Result { get; set; }

        public abstract Error Error { get; set; }

        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        public override string ToString()
        {
            return $"<-- " +
                   (Error != null ? $"Error: {Error.Message} ({Error.Code})" : $"Ok: {Result}") +
                   (Id != null ? $" #{Id}" : string.Empty);
        }

        public static ResponseMessage Create(object result, Error error, string id)
        {
            if (error != null)
                return new ResponseErrorMessage
                {
                    Result = null,
                    Error = error,
                    Id = id
                };

            return new ResponseResultMessage
            {
                Result = result,
                Error = null,
                Id = id
            };
        }
    }
}