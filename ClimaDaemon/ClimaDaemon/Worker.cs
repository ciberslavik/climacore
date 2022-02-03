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
        private ApplicationBuilder _builder;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.Log(LogLevel.Information, "Start building the application...");
            _builder = new ApplicationBuilder();
            ThreadPool.SetMaxThreads(3,3);
            _builder.Initialize(false);
        }
    }
}