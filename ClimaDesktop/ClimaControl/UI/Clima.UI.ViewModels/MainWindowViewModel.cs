using System;
using System.Windows.Input;
using Clima.Basics;
using Clima.UI.Interface;
using Clima.UI.Interface.ViewModels;
using Clima.UI.Interface.Views.Dialogs;

namespace Clima.UI.ViewModels
{
    public class MainWindowViewModel: ObservableObject, IMainWindowViewModel
    {
        private readonly IViewFactory _viewFactory;
        private string _title;

        public MainWindowViewModel(IViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
            ShowConfiguration = new RelayCommand(o =>
            {
                var view = _viewFactory.CreateView<IConfigurationDialogView>();
                view.Show();

            });
            _title = "Hello";
        }
        public string Title
        {
            get => _title;
            set => Update(ref _title, value);
        }

        public ICommand ShowConfiguration { get; }
        public override void AcceptModification()
        {
            throw new NotImplementedException();
        }
    }
}
