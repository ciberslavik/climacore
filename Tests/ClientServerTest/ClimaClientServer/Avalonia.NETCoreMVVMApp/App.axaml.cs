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
        private Client client;
        private IDataSerializer _serializer;
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            _serializer = new NewtonsoftSerializer();
            client = new Client("127.0.0.1", 8080, _serializer);

            Message msg = new Message();
            msg.Name = "TestMessage";
            msg.Data = "Hello world client message data";
            
            client.SendMessage(msg);
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