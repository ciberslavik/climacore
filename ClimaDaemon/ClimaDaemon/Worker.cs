using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clima.ServiceContainer.CastleWindsor;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConsoleServer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        ApplicationBuilder _builder;
        
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _builder = new ApplicationBuilder();
            _builder.Initialize();
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at:{Time}", DateTime.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}