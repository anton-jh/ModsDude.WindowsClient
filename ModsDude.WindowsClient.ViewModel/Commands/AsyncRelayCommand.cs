using System.Windows.Input;

namespace ModsDude.WindowsClient.ViewModel.Commands;
public class AsyncRelayCommand : ICommand
{
    private readonly Func<Task> _action;


    public AsyncRelayCommand(Func<Task> action)
    {
        _action = action;
    }


    public event EventHandler? CanExecuteChanged;


    public Func<bool> CanExecuteDelegate { get; init; } = CanAlwaysExecute;
    public Action<Exception>? ErrorHandlerDelegate { get; init; } = null;


    public bool CanExecute(object? parameter)
    {
        return CanExecuteDelegate();
    }

    public async void Execute(object? parameter)
    {
        try
        {
            await _action();
        }
        catch (Exception ex)
        {
            ErrorHandlerDelegate?.Invoke(ex);
        }
    }


    private static bool CanAlwaysExecute()
    {
        return true;
    }
}
