using System.Windows.Input;

namespace Clima.UI.Interface.ViewModels.Security
{
    public interface ILoginWindowViewModel:IViewModel
    {
        string Title { get; set; }
        string Login { get; set; }
        string Password { get; set; }
        bool RememberMe { get; set; }
        ICommand AcceptCommand { get; set; }
    }
}