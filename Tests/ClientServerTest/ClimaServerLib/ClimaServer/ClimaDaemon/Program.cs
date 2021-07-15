using System;
using System.Net;
using Clima.ServiceContainer.CastleWindsor;

namespace ConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationBuilder builder = new ApplicationBuilder();
            builder.Initialize();
            
            builder.Run();
        }
    }
}