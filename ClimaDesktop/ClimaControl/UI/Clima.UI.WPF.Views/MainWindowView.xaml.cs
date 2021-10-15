using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Clima.UI.Interface.ViewModels;
using Clima.UI.Interface.Views;

namespace Clima.UI.WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window, IMainWindowView
    {
        public MainWindowView(IMainWindowViewModel viewModel)
        {
            InitializeComponent();
        }
    }
}
