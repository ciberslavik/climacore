﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Windows;

namespace ClimaUpdater
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Updater.ClimaUpdater upd = new Updater.ClimaUpdater();

            upd.Init();
            base.OnStartup(e);
        }
    }
}
