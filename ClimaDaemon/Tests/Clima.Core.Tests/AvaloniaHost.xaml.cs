using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Clima.Core.Tests.IOService;

namespace Clima.Core.Tests
{
    public class AvaloniaHost:Application
    {
        public AvaloniaHost()
        {
            Console.WriteLine("Create avalonia app");
        }
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
        public IOSimulatorViewModel ViewModel { get; set; }

        public void SetVm(IOSimulatorViewModel vm)
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                IOSimulatorView view = new IOSimulatorView();
                view.DataContext = vm;
                view.Show();
            }
        }
        public override void OnFrameworkInitializationCompleted()
        {
            
            base.OnFrameworkInitializationCompleted();
        }
        
    }
}