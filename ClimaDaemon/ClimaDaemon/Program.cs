using System;
using System.Net;
using System.Threading;
using Clima.Core.Devices;
using Clima.ServiceContainer.CastleWindsor;

namespace ConsoleServer
{
    class Program
    {
        private static Timer _tmr;
        private static ApplicationBuilder _builder;
        static void Main(string[] args)
        {
            
            _builder = new ApplicationBuilder();
            _builder.Initialize();
            bool exitSignal = false;
            //_tmr = new Timer(TimerTimeout, null, 500, 500);
            while (!exitSignal)
            {
                
                var line = Console.ReadLine();
                if (line != null)
                {
                    if (line.Contains("exit"))
                        exitSignal = true;
                    if (Double.TryParse(line, out var result))
                    {
                        _builder.SetValue(result);
                    }
                    Console.WriteLine($"Reading:{line}");
                }
            }
        }

        private static void TimerTimeout(object? state)
        {
            DrawSensors(_builder.Sensors);
        }

        static void DrawSensors(ISensors sensors)
        {
            Console.Clear();
            Console.SetCursorPosition(Console.WindowLeft,Console.WindowTop);
            
            Console.WriteLine($"Front:{sensors.FrontTemperature}        Rear:{sensors.RearTemperature}");
            Console.WriteLine($"Outdoor:{sensors.OutdoorTemperature}");
            Console.WriteLine($"Humidity:{sensors.Humidity}        Pressure:{sensors.Pressure}");
        }
    }
}