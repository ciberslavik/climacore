using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using Clima.Basics.Services;


namespace Clima.Logger
{
    public class ConsoleSystemLogger : ISystemLogger
    {
        private object _lock = new object();
        public void Debug(string message, 
            [CallerFilePath]string callerFile = "", 
            [CallerMemberName]string callerName = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            
            var typeName = Path.GetFileNameWithoutExtension(callerFile);
            lock (_lock)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"[Debug]:");
                Console.ForegroundColor = ConsoleColor.Gray;
                //Console.WriteLine(
                //    $"{typeName}.{callerName}[{Thread.CurrentThread.ManagedThreadId}]:{callerFile} - {lineNumber}");
                Console.WriteLine($"\t{message}");
            }
        }

        public void Info(string message,
            [CallerFilePath]string callerFile = "", 
            [CallerMemberName]string callerName = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            var typeName = Path.GetFileNameWithoutExtension(callerFile);

            lock (_lock)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("[Info]: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                //Console.WriteLine(
                //    $"{typeName}.{callerName}[{Thread.CurrentThread.ManagedThreadId}]:{callerFile} - {lineNumber}");
                Console.WriteLine($"\t{message}");
            }
        }

        public void Error(string message, 
            [CallerFilePath]string callerFile = "", 
            [CallerMemberName]string callerName = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            var typeName = Path.GetFileNameWithoutExtension(callerFile);
            lock (_lock)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[Error]: ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(
                    $"{typeName}.{callerName}[{Thread.CurrentThread.ManagedThreadId}]:{callerFile} - {lineNumber}");
                Console.WriteLine($"\t{message}");
            }
        }

        public void System(string message, 
            [CallerFilePath]string callerFile = "", 
            [CallerMemberName]string callerName = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            var typeName = Path.GetFileNameWithoutExtension(callerFile);
            lock (_lock)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("[System]: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(
                    $"{typeName}.{callerName}[{Thread.CurrentThread.ManagedThreadId}]:{callerFile} - {lineNumber}");
                Console.WriteLine($"\t{message}");
            }
        }

        public void Exception(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[Exception]: {e.ToString()}");
            //Console.WriteLine($"[Exception]: {e.Message} \n {e.Source} \n {e.StackTrace}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}