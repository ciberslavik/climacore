using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Clima.Core.Tests.IOService
{
    public class IOSimulatorView : Window
    {
        public IOSimulatorView()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}