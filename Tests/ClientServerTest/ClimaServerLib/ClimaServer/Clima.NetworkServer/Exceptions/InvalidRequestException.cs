using System;
using Clima.NetworkServer.Messages;

namespace Clima.NetworkServer.Exceptions
{
    public class InvalidRequestException:JsonServicesException
    {
        public const int ErrorCode = -32600;

        public InvalidRequestException(string data)
            : base(ErrorCode, $"Invalid request. Request data: {data}")
        {
        }

        public InvalidRequestException(string data, Exception innerException)
            : base(ErrorCode, $"Invalid request. Request data: {data}")
        {
        }

        public InvalidRequestException(Error error)
            : base(ErrorCode, error.Message)
        {
            Details = error.Data;
        }

        internal InvalidRequestException()
            : this("test")
        {
            // for unit tests
        }
        
    }
}