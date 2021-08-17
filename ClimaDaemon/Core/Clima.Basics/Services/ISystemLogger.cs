using System.Runtime.CompilerServices;

namespace Clima.Basics.Services
{
    public interface ISystemLogger
    {
        void Debug(string message, [CallerFilePath]string callerFile = "", [CallerMemberName] string callerName = "");
        void Info(string message, [CallerFilePath]string callerFile = "", [CallerMemberName] string callerName = "");
        void Error(string message, [CallerFilePath]string callerFile = "", [CallerMemberName] string callerName = "");
        void System(string message, [CallerFilePath]string callerFile = "", [CallerMemberName] string callerName = "");
    }
}