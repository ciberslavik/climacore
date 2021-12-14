using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Clima.Basics.Services;

namespace Clima.Core
{
    public class LogFileWriter:ISystemLogger
    {
        private readonly string _filePath;
        private object _lock = new object();
        public LogFileWriter(string fileName)
        {
            var appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (appDir is not null)
            {
                _filePath = Path.Combine(appDir, fileName);
            }

            if (_filePath is null)
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    _filePath = Path.Combine(@"C:\", fileName);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    _filePath = Path.Combine("/home/", fileName);
                }
                else
                {
                    _filePath = "";
                }
            }
        }

        public void WriteLine(string text)
        {
            Write(text + "\n");
        }

        public void Write(string text)
        {
            if (File.Exists(_filePath))
            {
                using var tw = File.AppendText(_filePath);
                tw.Write(text + "\n");
            }
            else
            {
                using var tw = File.CreateText(_filePath);
                tw.Write(text + "\n");
            }
        }

        public void Debug(string message, string callerFile = "", string callerName = "", int lineNumber = 0)
        {
            var typeName = Path.GetFileNameWithoutExtension(callerFile);
            lock (_lock)
            {
                Write($"[Debug]:");
                WriteLine(
                    $"{typeName}.{callerName}[{Thread.CurrentThread.ManagedThreadId}]:{callerFile} - {lineNumber}");
                WriteLine($"\t{message}");
            }
        }

        public void Info(string message, string callerFile = "", string callerName = "", int lineNumber = 0)
        {
            var typeName = Path.GetFileNameWithoutExtension(callerFile);
            lock (_lock)
            {
                Write($"[Info]:");
                WriteLine(
                    $"{typeName}.{callerName}[{Thread.CurrentThread.ManagedThreadId}]:{callerFile} - {lineNumber}");
                WriteLine($"\t{message}");
            }
        }

        public void Error(string message, string callerFile = "", string callerName = "", int lineNumber = 0)
        {
            var typeName = Path.GetFileNameWithoutExtension(callerFile);
            lock (_lock)
            {
                Write($"[Error]:");
                WriteLine(
                    $"{typeName}.{callerName}[{Thread.CurrentThread.ManagedThreadId}]:{callerFile} - {lineNumber}");
                WriteLine($"\t{message}");
            }
        }

        public void System(string message, string callerFile = "", string callerName = "", int lineNumber = 0)
        {
            var typeName = Path.GetFileNameWithoutExtension(callerFile);
            lock (_lock)
            {
                Write($"[System]:");
                WriteLine(
                    $"{typeName}.{callerName}[{Thread.CurrentThread.ManagedThreadId}]:{callerFile} - {lineNumber}");
                WriteLine($"\t{message}");
            }
        }

        public void Exception(Exception e)
        {
            throw new NotImplementedException();
        }
    }
}