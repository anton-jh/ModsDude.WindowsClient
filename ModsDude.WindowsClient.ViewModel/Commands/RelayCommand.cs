using System.Windows.Input;

namespace ModsDude.WindowsClient.ViewModel.Commands;
public class RelayCommand : ICommand
{
    private readonly Action _action;
    private readonly Func<bool> _canExecute;


    public RelayCommand(Action action, Func<bool>? canExecute = null)
    {
        _action = action;
        _canExecute = canExecute ?? CanAlwaysExecute;
    }


    public event EventHandler? CanExecuteChanged;


    public bool CanExecute(object? parameter)
    {
        return _canExecute();
    }

    public void Execute(object? parameter)
    {
        _action();
    }


    private static bool CanAlwaysExecute()
    {
        return true;
    }
}
