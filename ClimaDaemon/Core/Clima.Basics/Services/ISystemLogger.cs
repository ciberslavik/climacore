using System.Runtime.CompilerServices;

namespace Clima.Basics.Services
{
    public interface ISystemLogger
    {
        void Debug(string message, [CallerMemberName] string callerName = "");
        void Info(string message, [CallerMemberName] string callerName = "");
        void Error(string message, [CallerMemberName] string callerName = "");
        void System(string message, [CallerMemberName] string callerName = "");
    }
}