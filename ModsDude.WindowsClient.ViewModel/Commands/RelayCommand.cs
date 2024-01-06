using System.Windows.Input;

namespace ModsDude.WindowsClient.ViewModel.Commands;
public class RelayCommand(Action action, Func<bool>? canExecute = null) : ICommand
{
    public event EventHandler? CanExecuteChanged;


    public bool CanExecute(object? parameter)
    {
        return canExecute?.Invoke() ?? true;
    }

    public void Execute(object? parameter)
    {
        action();
    }
}
