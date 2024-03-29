﻿using System;
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
using Clima.UI.Interface.ViewModels.Dialogs;
using Clima.UI.Interface.Views.Dialogs;

namespace Clima.UI.WPF.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для ConfigurationDialogView.xaml
    /// </summary>
    public partial class ConfigurationDialogView : Window, IConfigurationDialogView
    {
        public ConfigurationDialogView(IConfigurationDialogViewModel vm)
        {
            InitializeComponent();
        }
    }
}
