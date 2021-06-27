using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.NETCoreMVVMApp.ViewModels;
using Avalonia.NETCoreMVVMApp.Views;
using Clima.TcpClient;
using DataContract;
using NewtonsoftJsonSerializer;

namespace Avalonia.NETCoreMVVMApp
{
    public class App : Application
    {
        private ClimaClient _client;
        private IDataSerializer _serializer;
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            ClientOption option = new ClientOption()
            {
                Host = "127.0.0.1",
                Port = 5911
            };
            
            _client = new ClimaClient(option);
            _client.DataReceived += OnDataReceived;
            _client.Connect();
            
            _client.Send("Hello Clima C# Client");
            
        }

        private void OnDataReceived(DataReceivedEventArgs ea)
        {
            Console.WriteLine($"Data:{ea.Data}");
        }
        private void OnMessage(string message)
        {
            Console.WriteLine(message);
        }
        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var mw = new MainWindow();
                mw.DataContext = new MainWindowViewModel();
                desktop.MainWindow = mw;
                ;
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}