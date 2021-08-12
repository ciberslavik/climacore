using System;

namespace Clima.Core.Exceptions
{
    public class GraphProviderException : Exception
    {
        public GraphProviderException(string message = "") : base(message)
        {
        }
    }
}