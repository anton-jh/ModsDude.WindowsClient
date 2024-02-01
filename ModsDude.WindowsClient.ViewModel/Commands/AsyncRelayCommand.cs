using System.Windows.Input;

namespace ModsDude.WindowsClient.ViewModel.Commands;
public class AsyncRelayCommand(Func<Task> action)
    : ICommand
{
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
            await action();
        }
        catch (Exception ex)
        {
            if (ErrorHandlerDelegate is not null)
            {
                ErrorHandlerDelegate.Invoke(ex);
                return;
            }

            throw;
        }
    }


    private static bool CanAlwaysExecute()
    {
        return true;
    }
}
