using System;
using Clima.Basics.Configuration;
using Clima.Basics.Services;
using IServiceProvider = Clima.Basics.Services.IServiceProvider;

namespace Clima.Basics
{
    public class ClimaContext
    {
        private static readonly object _lock = new object();
        private static ClimaContext _instance;
        private static bool _exitSignal;
        private static object _exitLocker = new object();

        private readonly IServiceProvider _serviceProvider;

        private ClimaContext(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            Logger = _serviceProvider.Resolve<ISystemLogger>();
        }

        public static void InitContext(IServiceProvider serviceProvider)
        {
            if (_instance == null)
                lock (_lock)
                {
                    if (_instance == null) _instance = new ClimaContext(serviceProvider);
                }
        }

        public static ClimaContext Current
        {
            get
            {
                if (_instance == null)
                    throw new InvalidOperationException("Get context before initialize.");

                return _instance;
            }
        }

        public IConfigurationStorage ConfigurationStorage =>
            _serviceProvider.Resolve<IConfigurationStorage>();

        public IFileSystem FileSystem =>
            _serviceProvider.Resolve<IFileSystem>();

        public ISystemLogger Logger { get; private set; }
    }
}