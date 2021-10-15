﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Clima.Bootstrapper;

namespace ClimaLauncher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ClimaBootstrapper bs;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            bs = new ClimaBootstrapper();
            bs.Initialize();
            
            bs.Run();

        }
    }
}
