﻿using System;
using System.Net.Sockets;
using Clima.Basics.Configuration;
using Clima.Basics.Services;
using Clima.Core.Devices;
using IServiceProvider = Clima.Basics.Services.IServiceProvider;

namespace Clima.Core
{
    public class ClimaContext
    {
        private static readonly object _lock = new object();
        private static ClimaContext? _instance;
        private static object _exitLocker = new object();

        private readonly IServiceProvider _serviceProvider;
        private readonly ISensors _sensors;
        private readonly IConfigurationStorage _configuration;
        private ClimaContext(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            Logger = _serviceProvider.Resolve<ISystemLogger>() ?? new LogFileWriter("ClimaContext.log");
            _sensors = _serviceProvider.Resolve<ISensors>();
            _configuration = _serviceProvider.Resolve<IConfigurationStorage>();
        }

        static ClimaContext()
        {
            Logger = new LogFileWriter("ClimaContext.log");
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
            _configuration;

        public IFileSystem FileSystem =>
            _serviceProvider.Resolve<IFileSystem>();

        public static ISystemLogger Logger { get; set; }

        public ISensors Sensors => _sensors;

        public void SaveConfiguration()
        {
            _configuration.Save();
        }
    }
}