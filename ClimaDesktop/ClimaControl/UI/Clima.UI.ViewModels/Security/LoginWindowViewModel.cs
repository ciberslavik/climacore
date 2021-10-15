using System.Windows.Input;
using Clima.Basics;
using Clima.UI.Interface.ViewModels.Security;

namespace Clima.UI.ViewModels.Security
{
    public class LoginWindowViewModel:ObservableObject, ILoginWindowViewModel
    {
        private string _login;
        private string _password;
        private bool _rememberMe;
        private ICommand _acceptCommand;
        private string _title;

        public string Title
        {
            get => _title;
            set => Update(ref _title, value);
        }

        public string Login
        {
            get => _login;
            set => Update(ref _login, value);
        }

        public string Password
        {
            get => _password;
            set => Update(ref _password, value);
        }

        public bool RememberMe
        {
            get => _rememberMe;
            set => Update(ref _rememberMe, value);
        }

        public ICommand AcceptCommand
        {
            get => _acceptCommand;
            set => Update(ref _acceptCommand, value);
        }

        public override void AcceptModification()
        {
            throw new System.NotImplementedException();
        }
    }
}