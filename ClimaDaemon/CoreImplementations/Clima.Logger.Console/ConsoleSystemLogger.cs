using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using Clima.Basics.Services;


namespace Clima.Logger
{
    public class ConsoleSystemLogger : ISystemLogger
    {
        public void Debug(string message, 
            [CallerFilePath]string callerFile = "", 
            [CallerMemberName]string callerName = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            var typeName = Path.GetFileNameWithoutExtension(callerFile);
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"[Debug]:");
            Console.ForegroundColor = defaultColor;
            Console.WriteLine($"{callerFile}:{lineNumber}");
            Console.WriteLine($"  {typeName}.{callerName}[{Thread.CurrentThread.ManagedThreadId}]");
            Console.WriteLine($"\t{message}");
        }

        public void Info(string message,
            [CallerFilePath]string callerFile = "", 
            [CallerMemberName]string callerName = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            var typeName = Path.GetFileNameWithoutExtension(callerFile);
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[Info]: ");
            Console.ForegroundColor = defaultColor;
            Console.WriteLine($"{callerFile}:{lineNumber}");
            Console.WriteLine($"  {typeName}.{callerName}[{Thread.CurrentThread.ManagedThreadId}]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\t{message}");
            Console.ForegroundColor = defaultColor;
        }

        public void Error(string message, 
            [CallerFilePath]string callerFile = "", 
            [CallerMemberName]string callerName = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            var typeName = Path.GetFileNameWithoutExtension(callerFile);
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[Error]: ");
            Console.ForegroundColor = defaultColor;
            Console.WriteLine($"{callerFile}:{lineNumber}");
            Console.WriteLine($"  {typeName}.{callerName}[{Thread.CurrentThread.ManagedThreadId}]");
            Console.WriteLine($"\t{message}");
        }

        public void System(string message, 
            [CallerFilePath]string callerFile = "", 
            [CallerMemberName]string callerName = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            var typeName = Path.GetFileNameWithoutExtension(callerFile);
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[Debug]: ");
            Console.ForegroundColor = defaultColor;
            Console.WriteLine($"{callerFile}:{lineNumber}");
            Console.WriteLine($"  {typeName}.{callerName}[{Thread.CurrentThread.ManagedThreadId}]");
            Console.WriteLine($"\t{message}");
        }

        public void Exception(Exception e)
        {
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[Exception]: {e.ToString()}");
            //Console.WriteLine($"[Exception]: {e.Message} \n {e.Source} \n {e.StackTrace}");
            Console.ForegroundColor = defaultColor;
        }
    }
}