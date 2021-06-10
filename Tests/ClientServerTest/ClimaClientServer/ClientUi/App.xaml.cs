using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Clima.TcpClient;

namespace ClientUi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ClientHost _host;
        protected override void OnStartup(StartupEventArgs e)
        {
            _host = new ClientHost(OnClientMessage);
            _host.RunClientThread();
            
            base.OnStartup(e);
        }

        private void OnClientMessage(string message)
        {
            Console.WriteLine($"Client:{message}");
        }
    }
}