using System;
using System.Runtime.CompilerServices;

namespace Clima.Basics.Services
{
    public interface ISystemLogger
    {
        void Debug(string message,
            [CallerFilePath]string callerFile = "",
            [CallerMemberName] string callerName = "",
            [CallerLineNumber] int lineNumber = 0);
        void Info(string message, 
            [CallerFilePath]string callerFile = "",
            [CallerMemberName] string callerName = "",
            [CallerLineNumber] int lineNumber = 0);
        void Error(string message,
            [CallerFilePath]string callerFile = "",
            [CallerMemberName] string callerName = "",
            [CallerLineNumber] int lineNumber = 0);
        void System(string message, 
            [CallerFilePath]string callerFile = "", 
            [CallerMemberName] string callerName = "",
            [CallerLineNumber] int lineNumber = 0);

        void Exception(Exception e);
    }
}