using System;

namespace ConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ClimaServer server = new ClimaServer();

            server.Start();
            
            Console.WriteLine("Hello World!");
            Console.ReadKey();

            server.Stop();
        }
    }
}