using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleServer
{
    internal class Program
    {
        private static Worker2 _wrk;
        public static void Main(string[] args)
        {
           /* _wrk = new Worker2();

            _wrk.Run();

            Console.ReadLine();*/
           CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSystemd()
                .ConfigureServices((hostContext, services) => { services.AddHostedService<Worker>(); });
    }
}