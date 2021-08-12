using System;
using Clima.Basics.Services.Communication.Messages;

namespace Clima.Basics.Services.Communication.Exceptions
{
    [Serializable]
    public class InternalErrorException : JsonServicesException
    {
        public const int ErrorCode = -32603;

        public InternalErrorException(string message)
            : base(ErrorCode, $"Internal error: {message}")
        {
        }

        public InternalErrorException(Error error)
            : base(ErrorCode, error.Message)
        {
            Details = error.Data;
        }

        internal InternalErrorException()
            : this("test")
        {
            // for unit tests
        }
    }
}