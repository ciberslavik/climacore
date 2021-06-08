#nullable enable
namespace Clima.Services.Commands
{
    public interface ICommand
    {
        void Execute(object? parameter);
        bool CanExecute(object? parameter);
    }
}