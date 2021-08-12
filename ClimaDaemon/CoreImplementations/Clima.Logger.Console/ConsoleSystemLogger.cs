using System;
using Clima.Basics.Services;
using console = System.Console;

namespace Clima.Logger.Console
{
    public class ConsoleSystemLogger : ISystemLogger
    {
        public void Debug(string message, string callerName = "")
        {
            console.WriteLine($"[Debug]{callerName}:{message}");
        }

        public void Info(string message, string callerName = "")
        {
            console.WriteLine($"[Info]{callerName}:{message}");
        }

        public void Error(string message, string callerName = "")
        {
            console.WriteLine($"[Error]{callerName}:{message}");
        }

        public void System(string message, string callerName = "")
        {
            console.WriteLine($"[System]{callerName}:{message}");
        }
    }
}