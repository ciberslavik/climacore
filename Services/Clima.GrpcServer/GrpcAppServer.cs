using System;
using System.Threading;
using Clima.Services.Communication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Clima.GrpcServer
{
    public class GrpcAppServer
    {
        Thread _serverThread;
        public void Start()
        {
            _serverThread = new Thread((e)=>
            {
                CreateHostBuilder(new string[] { }).Build().Run(); 
            });
            _serverThread.Start();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }); 
    }
}
