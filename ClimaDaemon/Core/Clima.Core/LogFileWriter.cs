using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Clima.Core
{
    public class LogFileWriter
    {
        private readonly string _filePath;
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
    }
}