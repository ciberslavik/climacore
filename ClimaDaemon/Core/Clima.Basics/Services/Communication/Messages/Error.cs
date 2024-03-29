using System;
using System.Runtime.Serialization;
using Clima.Basics.Services.Communication.Exceptions;

namespace Clima.Basics.Services.Communication.Messages
{
    public class Error
    {
        public Error(Exception ex = null)
        {
            if (ex is JsonServicesException jx) Code = jx.Code;

            if (ex != null)
            {
                Message = ex.Message;
                Data = ex.ToString();
            }
        }

        [DataMember(Name = "code")] public int Code { get; set; }

        [DataMember(Name = "message")] public string Message { get; set; }

        [DataMember(Name = "data", EmitDefaultValue = false)]
        public object Data { get; set; }
    }
}