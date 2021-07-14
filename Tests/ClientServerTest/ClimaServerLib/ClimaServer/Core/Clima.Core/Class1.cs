using System;
using Clima.Basics.Configuration;
using IServiceProvider = Clima.Basics.Services.IServiceProvider;

namespace Clima.Core
{
    public class AppContext
    {
        private static readonly object _lock = new object();
        private static AppContext _instance;

        private readonly IServiceProvider _serviceProvider;

        private AppContext(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static void InitContext(IServiceProvider serviceProvider)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new AppContext(serviceProvider);
                    }
                }
            }
        }

        public static AppContext Current
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
        
        
    }
}