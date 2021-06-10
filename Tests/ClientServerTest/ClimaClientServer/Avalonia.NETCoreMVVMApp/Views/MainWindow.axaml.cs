using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Clima.TcpClient;
using DataContract;

namespace Avalonia.NETCoreMVVMApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
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