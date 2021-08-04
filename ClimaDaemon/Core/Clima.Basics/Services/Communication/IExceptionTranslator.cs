using System;
using Clima.Basics.Services.Communication.Messages;

namespace Clima.Basics.Services.Communication
{
    public interface IExceptionTranslator
    {
        Error Translate(Exception ex, int? code = null, string message = null);
    }
}